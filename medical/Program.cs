using System;
using System.Collections.Generic;
using System.IO;

class Medicine
{
    public int MedicineId;
    public string MedicineName;
    public float Price;
    public int Quantity;
}

class MedicalStore
{
    private string filePath;

    public MedicalStore(string filePath)
    {
        this.filePath = filePath;
    }

    public List<Medicine> GetMedicines()
    {
        List<Medicine> medicines = new List<Medicine>();
        try
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] data = line.Split(',');
                int medicineId = Convert.ToInt32(data[0]);
                string medicineName = data[1];
                float price = float.Parse(data[2]);
                int quantity = Convert.ToInt32(data[3]);

                if (quantity <= 20 || price < 500)
                {
                    Medicine medicine = new Medicine
                    {
                        MedicineId = medicineId,
                        MedicineName = medicineName,
                        Price = price,
                        Quantity = quantity
                    };
                    medicines.Add(medicine);
                }
            }
        }
        catch (FileNotFoundException)
        {
            throw new NoMedicineFoundException("No medicines to order at present");
        }
        return medicines;
    }

    public float GetPurchaseAmount(List<Medicine> medicines)
    {
        float totalAmount = 0;
        foreach (Medicine medicine in medicines)
        {
            totalAmount += medicine.Price * medicine.Quantity;
        }
        return totalAmount;
    }
}

class NoMedicineFoundException : Exception
{
    public NoMedicineFoundException(string message) : base(message) { }
}

class Program
{
    static void Main()
    {
        string filePath = "medicines.txt";
        MedicalStore medicalStore = new MedicalStore(filePath);

        try
        {
            List<Medicine> medicines = medicalStore.GetMedicines();
            if (medicines.Count == 0)
            {
                throw new NoMedicineFoundException("No medicines to order at present");
            }

            Console.WriteLine("Medicines to be purchae:");
            foreach (Medicine medicine in medicines)
            {
                Console.WriteLine(medicine.MedicineId);
                Console.WriteLine(medicine.MedicineName);
                Console.WriteLine(medicine.Price);
                Console.WriteLine(medicine.Quantity);
            }


            float totalPurchaseAmount = medicalStore.GetPurchaseAmount(medicines);
            Console.WriteLine("Total purchase amount:");

        }
        catch (NoMedicineFoundException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

