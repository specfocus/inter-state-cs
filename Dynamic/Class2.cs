using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace XState.Dynamic
{
    // This form demonstrates using a BindingSource to bind  
    // a list to a DataGridView control. The list does not  
    // raise change notifications. However the DemoCustomer type   
    // in the list does.  
    public partial class Form1 : Form
    {
        // This button causes the value of a list element to be changed.  
        private Button changeItemBtn = new Button();

        // This DataGridView control displays the contents of the list.  
        private DataGridView customersDataGridView = new DataGridView();

        // This BindingSource binds the list to the DataGridView control.  
        private BindingSource customersBindingSource = new BindingSource();

        public Form1()
        {
            InitializeComponent();

            // Set up the "Change Item" button.  
            changeItemBtn.Text = "Change Item";
            changeItemBtn.Dock = DockStyle.Bottom;
            changeItemBtn.Click +=
                new EventHandler(changeItemBtn_Click);
            this.Controls.Add(changeItemBtn);

            // Set up the DataGridView.  
            customersDataGridView.Dock = DockStyle.Top;
            this.Controls.Add(customersDataGridView);

            this.Size = new Size(400, 200);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Create and populate the list of DemoCustomer objects  
            // which will supply data to the DataGridView.  
            BindingList<DemoCustomer> customerList = new BindingList<DemoCustomer>();
            customerList.Add(DemoCustomer.CreateNewCustomer());
            customerList.Add(DemoCustomer.CreateNewCustomer());
            customerList.Add(DemoCustomer.CreateNewCustomer());

            // Bind the list to the BindingSource.  
            customersBindingSource.DataSource = customerList;

            // Attach the BindingSource to the DataGridView.  
            customersDataGridView.DataSource =
                customersBindingSource;

        }

        // Change the value of the CompanyName property for the first   
        // item in the list when the "Change Item" button is clicked.  
        void changeItemBtn_Click(object sender, EventArgs e)
        {
            // Get a reference to the list from the BindingSource.  
            BindingList<DemoCustomer> customerList =
                customersBindingSource.DataSource as BindingList<DemoCustomer>;

            // Change the value of the CompanyName property for the   
            // first item in the list.  
            customerList[0].CustomerName = "Tailspin Toys";
            customerList[0].PhoneNumber = "(708)555-0150";
        }

    }

    // This is a simple customer class that   
    // implements the IPropertyChange interface.  
    public class DemoCustomer : INotifyPropertyChanged
    {
        // These fields hold the values for the public properties.  
        private Guid idValue = Guid.NewGuid();
        private string customerNameValue = string.Empty;
        private string phoneNumberValue = string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;

        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // The constructor is private to enforce the factory pattern.  
        private DemoCustomer()
        {
            customerNameValue = "Customer";
            phoneNumberValue = "(312)555-0100";
        }

        // This is the public factory method.  
        public static DemoCustomer CreateNewCustomer()
        {
            return new DemoCustomer();
        }

        // This property represents an ID, suitable  
        // for use as a primary key in a database.  
        public Guid ID
        {
            get
            {
                return idValue;
            }
        }

        public string CustomerName
        {
            get
            {
                return customerNameValue;
            }

            set
            {
                if (value != customerNameValue)
                {
                    customerNameValue = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string PhoneNumber
        {
            get
            {
                return phoneNumberValue;
            }

            set
            {
                if (value != phoneNumberValue)
                {
                    phoneNumberValue = value;
                    NotifyPropertyChanged();
                }
            }
        }
    }
}
