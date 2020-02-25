using System.Windows.Forms;

namespace CosmoKids
{
    class CosmoKids : Client
    {
        private string date_of_agreement;
        private string father_name;
        private string father_address;
        private string father_home_phone;
        private string father_cell_phone;
        private string father_email;
        private string father_place_of_employment;
        private string father_work_phone;
        private string mother_name;
        private string mother_address;
        private string mother_home_phone;
        private string mother_cell_phone;
        private string mother_email;
        private string mother_place_of_employment;
        private string mother_work_phone;
        private string child_name_doctor;
        private string child_address_doctor;
        private string child_phone_doctor;
        private string child_allergies;
        private string emergency_person1_name;
        private string emergency_person1_phone;
        private string emergency_person1_relationchip_to_child;
        private string emergency_person2_name;
        private string emergency_person2_phone;
        private string emergency_person2_relationchip_to_child;
        private string person_authorized_pickup1;
        private string person_authorized_pickup2;
        private const string duration_of_trial_period = "1 month";
        private string end_of_trial_period;
        private double sum_of_payment;

        //Properties

        public string Date_of_agreement
        {
            get { return date_of_agreement; }
            set { date_of_agreement = value; }
        }

        public string Father_name
        {
            get { return father_name; }
            set { father_name = value; }
        }

        public string Father_address
        {
            get { return father_address; }
            set { father_address = value; }
        }

        public string Father_home_phone
        {
            get { return father_home_phone; }
            set { father_home_phone = value; }
        }

        public string Father_cell_phone
        {
            get { return father_cell_phone; }
            set { father_cell_phone = value; }
        }

        public string Father_email
        {
            get { return father_email; }
            set { father_email = value; }
        }

        public string Father_place_of_employment
        {
            get { return father_place_of_employment; }
            set { father_place_of_employment = value; }
        }

        public string Father_work_phone
        {
            get { return father_work_phone; }
            set { father_work_phone = value; }
        }

        public string Mother_name
        {
            get { return mother_name; }
            set { mother_name = value; }
        }

        public string Mother_address
        {
            get { return mother_address; }
            set { mother_address = value; }
        }

        public string Mother_home_phone
        {
            get { return mother_home_phone; }
            set { mother_home_phone = value; }
        }

        public string Mother_cell_phone
        {
            get { return mother_cell_phone; }
            set { mother_cell_phone = value; }
        }

        public string Mother_email
        {
            get { return mother_email; }
            set { mother_email = value; }
        }

        public string Mother_place_of_employment
        {
            get { return mother_place_of_employment; }
            set { mother_place_of_employment = value; }
        }

        public string Mother_work_phone
        {
            get { return mother_work_phone; }
            set { mother_work_phone = value; }
        }

        public string Child_name_doctor
        {
            get { return child_name_doctor; }
            set { child_name_doctor = value; }
        }

        public string Child_address_doctor
        {
            get { return child_address_doctor; }
            set { child_address_doctor = value; }
        }

        public string Child_phone_doctor
        {
            get { return child_phone_doctor; }
            set { child_phone_doctor = value; }
        }

        public string Child_allergies
        {
            get { return child_allergies; }
            set { child_allergies = value; }
        }

        public string Emergency_person1_name
        {
            get { return emergency_person1_name; }
            set { emergency_person1_name = value; }
        }

        public string Emergency_person1_phone
        {
            get { return emergency_person1_phone; }
            set { emergency_person1_phone = value; }
        }

        public string Emergency_person1_relationchip_to_child
        {
            get { return emergency_person1_relationchip_to_child; }
            set { emergency_person1_relationchip_to_child = value; }
        }

        public string Emergency_person2_name
        {
            get { return emergency_person2_name; }
            set { emergency_person2_name = value; }
        }

        public string Emergency_person2_phone
        {
            get { return emergency_person2_phone; }
            set { emergency_person2_phone = value; }
        }

        public string Emergency_person2_relationchip_to_child
        {
            get { return emergency_person2_relationchip_to_child; }
            set { emergency_person2_relationchip_to_child = value; }
        }

        public string Person_authorized_pickup1
        {
            get { return person_authorized_pickup1; }
            set { person_authorized_pickup1 = value; }
        }

        public string Person_authorized_pickup2
        {
            get { return person_authorized_pickup2; }
            set { person_authorized_pickup2 = value; }
        }

        public string End_of_trial_period
        {
            get { return end_of_trial_period; }
            set { end_of_trial_period = value; }
        }

        public double Sum_of_payment
        {
            get { return sum_of_payment; }
            set
            {
                if (value == 0)
                {
                    MessageBox.Show("Sum of payment can't be 0!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (value < 0)
                {
                    MessageBox.Show("Sum of payment can't be less 0!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    sum_of_payment = value;
                }
            }
        }

        public static string Duration_of_trial_period
        {
            get { return duration_of_trial_period; }
        }


    }
}
