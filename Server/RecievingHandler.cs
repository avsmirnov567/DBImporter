using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using System.Configuration;
using System.Windows.Forms;
using Common;
using Server.Model;

namespace Server
{
    class RecievingHandler
    {
        public static bool SendEncryptedSymmetricalKey(string rsaRequest)
        {
            bool result = false;

            DataBlob input = (DataBlob)Serializer.DeserializeObject(Convert.FromBase64String(rsaRequest));
            string queuePath = input.Data;

            byte[] symmetricalKey = Encryptor.GenerateSymmetricalKey();
            ConfigurationManager.AppSettings.Set("tdesKey", Convert.ToBase64String(symmetricalKey));
            byte[] encryptedKey = Encryptor.EncryptAsymmetrycal(symmetricalKey, input.Key);

            try
            {
                MessageQueue queue = null;
                if (!MessageQueue.Exists(queuePath))
                {
                    queue = MessageQueue.Create(queuePath);
                }
                else
                {
                    queue = new MessageQueue(queuePath);
                }
                queue.Send(Convert.ToBase64String(encryptedKey), "Key");
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

        public static int RecieveData(string input)
        {
            string preparedInput = input.Replace("\r\n", "\n");
            string[] rows = preparedInput.Split('\n');
            int sentTotal = 0;
            foreach (string r in rows)
            {
                try
                {
                    string[] fields = r.Split(';');
                    string doctor = fields[0];
                    string patient = fields[1];
                    string disease = fields[2];
                    string number = fields[3];
                    sentTotal += UpdateDatabase(doctor, patient, disease, number);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return sentTotal;
        }

        private static int UpdateDatabase(string p_doctor, string p_patient, string p_disease, string p_number)
        {
            int sent = 0;
            Diagnoses diagnose;
            Doctors doctor;
            Patients patient;
            Diseases disease;
            int number = int.Parse(p_number);
            using (MedicineContext db = new MedicineContext())
            {
                diagnose = db.Diagnoses.Where(d => d.Doctors.fullname == p_doctor && d.Diseases.name == p_disease
                && d.Patients.fullname == p_patient && d.number == number).FirstOrDefault();
                if (diagnose == null)
                {
                    doctor = db.Doctors.Where(d => d.fullname == p_doctor).FirstOrDefault();
                    if (doctor == null)
                    {
                        doctor = new Doctors()
                        {
                            id = Guid.NewGuid(),
                            fullname = p_doctor
                        };
                        sent += SaveToDatabase(doctor);
                    }
                    patient = db.Patients.Where(d => d.fullname == p_patient).FirstOrDefault();
                    if (patient == null)
                    {
                        patient = new Patients()
                        {
                            id = Guid.NewGuid(),
                            fullname = p_patient
                        };
                        sent += SaveToDatabase(patient);
                    }
                    disease = db.Diseases.Where(d => d.name == p_disease).FirstOrDefault();
                    if (disease == null)
                    {
                        disease = new Diseases()
                        {
                            id = Guid.NewGuid(),
                            name = p_disease
                        };
                        sent += SaveToDatabase(disease);
                    }
                    diagnose = new Diagnoses()
                    {
                        id = Guid.NewGuid(),
                        number = number,
                        patient = patient.id,
                        disease = disease.id,
                        doctor = doctor.id
                    };
                    sent += SaveToDatabase(diagnose);
                }
            }
            return sent;
        }

        private static int SaveToDatabase<T>(T tObject)
        {
            using (MedicineContext db = new MedicineContext())
            {
                if (tObject is Diagnoses)
                {
                    db.Diagnoses.Add(tObject as Diagnoses);
                }
                else if (tObject is Doctors)
                {
                    db.Doctors.Add(tObject as Doctors);
                }
                else if (tObject is Patients)
                {
                    db.Patients.Add(tObject as Patients);
                }
                else if (tObject is Diseases)
                {
                    db.Diseases.Add(tObject as Diseases);
                }
                else
                {
                    throw new TypeLoadException("Тип не распознан!");
                }
                return db.SaveChanges();
            }
        }
    }
}
