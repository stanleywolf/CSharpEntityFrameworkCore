using System.Linq;

namespace PetClinic.DataProcessor
{
    using System;

    using PetClinic.Data;

    public class Bonus
    {
        public static string UpdateVetProfession(PetClinicContext context, string phoneNumber, string newProfession)
        {
            var vet = context.Vets.FirstOrDefault(x => x.PhoneNumber == phoneNumber);

            if (vet == null)
            {
                return $"Vet with phone number {phoneNumber} not found!";
            }
            else
            {
                var oldProfession = vet.Profession;

                vet.Profession = newProfession;
                context.SaveChanges();

                return $"{vet.Name}'s profession updated from {oldProfession} to {newProfession}.";
            }

        }
    }
}
