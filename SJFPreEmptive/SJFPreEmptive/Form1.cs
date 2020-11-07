using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SJFPreEmptive
{
    public partial class Form1 : Form
    {
        static int textBoxCounter = 1;//input textboxes on add button
        static int ten = 10;//because c# is giving me an error that this must be a variable
        static int newTextBoxCounter = 1;//for looping textboxes in output

        List<TextBox> textboxLists = new List<TextBox>();//list of inputed textboxes using add button
        List<Label> labelLists = new List<Label>();//list of inputed labels using add button
        //List<Button> buttonLists = new List<Button>();

        List<int> textbox_value = new List<int>();//list of values from textboxlists using .Text
        List<int> waitingTime_value = new List<int>();//list of waiting time values
        List<int> sortedArray = new List<int>();//list for sorted values from textbox_value
        List<string> labelString = new List<string>();//list of labels of sorted values

        List<Label> processLabelList = new List<Label>();
        List<Label> executionLabelList = new List<Label>();
        List<Label> waitingLabelList = new List<Label>();

        float sum = 0;//for awt
        float sum2 = 0;//for aet
        public Form1()
        {
            InitializeComponent();
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void addBtn_Click(object sender, EventArgs e)
        {
            addingField();
        }

        private void calculateBtn_Click(object sender, EventArgs e)
        {
            try {
                foreach (var text in textboxLists)              //start
                {                                               // getting the values of inputted textboxes and storing it to another
                    textbox_value.Add(int.Parse(text.Text));    //list
                }                                               //end

                foreach (var text in labelLists)                //start
                {                                               //getting the labels using .Text and putting it into list
                    labelString.Add(text.Text);                 //
                }                                               //end

                sorting();  //self made method for sorting the values and labels
                solving();  //self made method for solving the waiting time

                for (int i = 1; i < textBoxCounter; i++)    //start 
                {                                           // outputting labels in outputPanel
                    add_label_in_outputPanel();             //
                }
                //end
                assign(); //self made method for assigning values in AWT and AET textbox
                ganttDesign(); //self made method for designing the gantt look stupid though
            }
            catch (Exception ex) {
                MessageBox.Show("You triggered a bug congratulations!");

            }
        }
        //start of adding field
        public void addingField() {
            try {
                TextBox tb = new TextBox();
                Label lb = new Label();
                Button button = new Button();

                Point point = new Point(150, ten + (50 * textBoxCounter));
                Point label_point = new Point(50, ten + (50 * textBoxCounter));
                Point button_point = new Point(280, ten + (50 * textBoxCounter));

                tb.Width = 100;
                tb.Name = ("textBox" + textBoxCounter).ToString();
               // tb.Text = (TextBox)insertPanel.Controls.(tb.Name);
                tb.Font = new Font(tb.Font.FontFamily, 12);
                tb.Location = point;

                lb.Text = ("Process" + textBoxCounter).ToString();
                lb.Name = ("process" + textBoxCounter).ToString();
                lb.Location = label_point;
                lb.ForeColor = Color.White;//change this later

                insertPanel.Controls.Add(tb);
                insertPanel.Controls.Add(lb);
                //insertPanel.Controls.Add(button);

                textboxLists.Add(tb);
                labelLists.Add(lb);
                //buttonLists.Add(button);

                textBoxCounter++;
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.Source);
            }
            
        }
        //end of adding field
        //start of adding label to output panel
        public void add_label_in_outputPanel() {
            try {

                Label process_label = new Label();
                Label executionTime_label = new Label();
                Label waitingTime_label = new Label();

                Point process_point = new Point(25, 60 * newTextBoxCounter);
                Point executionTime_point = new Point(200, 60 * newTextBoxCounter);
                Point waitingTime_point = new Point(320, 60 * newTextBoxCounter);

                //adding of label of the process
                process_label.Name = ("processTB" + newTextBoxCounter).ToString();
                process_label.Text = (labelString[newTextBoxCounter - 1]) + " :";
                process_label.ForeColor = Color.White;
                process_label.Location = process_point;
                outputPanel.Controls.Add(process_label);
                processLabelList.Add(process_label);

                //adding label of execution time
                executionTime_label.Name = ("exeTB" + newTextBoxCounter).ToString();
                executionTime_label.Text = (sortedArray[newTextBoxCounter - 1]).ToString();// i putted "-1" because compiler is complaining of out of index  exception and it just works fine
                executionTime_label.ForeColor = Color.White;
                executionTime_label.Location = executionTime_point;
                outputPanel.Controls.Add(executionTime_label);
                executionLabelList.Add(executionTime_label);

                //adding label of waiting time
                waitingTime_label.Name = ("waitingTB" + newTextBoxCounter).ToString();
                waitingTime_label.Text = (waitingTime_value[newTextBoxCounter - 1]).ToString();
                waitingTime_label.ForeColor = Color.White;
                waitingTime_label.Location = waitingTime_point;
                outputPanel.Controls.Add(waitingTime_label);
                waitingLabelList.Add(waitingTime_label);
                newTextBoxCounter++;
            }
            catch (Exception ex) {
                MessageBox.Show("You triggered a bug congratulations!");
            }
        }
        //end of adding label to output panel}
        public void solving() {
           try {
                int[] executeArray = sortedArray.ToArray();//converting lists to array
                int[] waitingTime = new int[executeArray.Length];//creating an array with a lenght based on the executeArray lenght

                waitingTime[0] = 0;//first index is always 0
                for (int i = 0; i < waitingTime.Length; i++)                    // start
                {                                                               //
                    if ((i + 1) < waitingTime.Length)                           //index algorithm to counter outofboundsexception
                    {                                                           //
                        waitingTime[i + 1] = waitingTime[i] + executeArray[i];  //solving the waiting time
                    }                                                           //end
                    waitingTime_value.Add(waitingTime[i]);                      // storing the array values to list
                }
  
       }
            catch (Exception ex) {
                MessageBox.Show("You triggered a bug congratulations!");

            }
        }
        public void sorting(){
           try {
                int[] sortExe = textbox_value.ToArray(); //converting list to an array
                string[] labelStorists = labelString.ToArray();//converting list to an array
                int temp;//for values
                string temp2;//for labels
                for (int x = 0; x < sortExe.Length; x++)
                {
                    for (int y = x + 1; y < sortExe.Length; y++)
                    {
                        if (sortExe[x] > sortExe[y])
                        {

                            temp = sortExe[x];
                            sortExe[x] = sortExe[y];
                            sortExe[y] = temp;

                            temp2 = labelStorists[x];
                            labelStorists[x] = labelStorists[y];
                            labelStorists[y] = temp2;

                        }
                    }
                    sortedArray.Add(sortExe[x]);//storing array values to a list
                    //labelString.Add(labelStorists[x]);
                    //Console.WriteLine(sortedArray[x]);
                }
                labelString = labelStorists.ToList();//converting array to list
                }
              catch (Exception ex) {
                   MessageBox.Show("You triggered a bug congratulations!");
               }

            }
        public void assign() {
            
            for (int sum_i = 0;sum_i < waitingTime_value.Count(); sum_i++) {        //algo for both AWT and AET
                sum = (sum + waitingTime_value[sum_i])/waitingTime_value.Count();   //adding values of each index
                sum2 = (sum2 + sortedArray[sum_i])/sortedArray.Count();             //dividing by how many indeces
            }
            awtTB.Text = sum.ToString("0.00");
            aetTB.Text = sum2.ToString("0.00");
        }


        private void delete_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void delete_MouseLeave(object sender, EventArgs e)
        {

        }

        private void delete_Click(object sender, EventArgs e)
        {
      
            foreach (TextBox txtBox in textboxLists) {
                insertPanel.Controls.Remove(txtBox);
            }
            foreach (Label lbl in labelLists)
            {
                insertPanel.Controls.Remove(lbl);
            }
            foreach (Label pOutLabel in processLabelList) {
                outputPanel.Controls.Remove(pOutLabel);
            }
            foreach (Label eOutLabel in executionLabelList)
            {
                outputPanel.Controls.Remove(eOutLabel);
            }
            foreach (Label wOutLabel in waitingLabelList)
            {
                outputPanel.Controls.Remove(wOutLabel);
            }
            for (int wtv = 0; wtv < waitingTime_value.Count(); wtv++) {
                waitingTime_value[wtv] = 0;
            }
            for (int sa = 0; sa < sortedArray.Count(); sa++) {
                sortedArray[sa] = 0;
            }
            for (int ls = 0; ls < labelString.Count(); ls++) {
                labelString[ls] = "";
            }


            textBoxCounter = 1;
            newTextBoxCounter = 1;
            textbox_value.Clear();
            sum = 0;
            sum2 = 0;
            awtTB.Clear();
            aetTB.Clear();

            textboxLists.Clear();
            labelLists.Clear();
            textbox_value.Clear();
            waitingTime_value.Clear();
            sortedArray.Clear();
            labelString.Clear();
            processLabelList.Clear();
            executionLabelList.Clear();
            waitingLabelList.Clear();
            gantt.Columns.Clear();

            //MessageBox.Show("Still not available.\n Please wait for next patch.");
        }
        public void ganttDesign() {
            Random rnd = new Random(0-256);
            gantt.ColumnCount = textBoxCounter - 1;
            for (int colCount = 0; colCount < textBoxCounter - 1; colCount++) {
                gantt.ColumnHeadersVisible = false;
                gantt.RowHeadersVisible = false;
                Color myColor = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));//random rgb number using Random()
                gantt.Rows[0].Cells[colCount].Style.BackColor = myColor;
                gantt.Rows[0].Cells[colCount].Value = labelString[colCount] +"            [" + waitingTime_value[colCount] + "]";
                gantt.Columns[colCount].Width = 30 * waitingTime_value[colCount];//setting the width
                gantt.Rows[0].Height = 90;
            }
        }
    }
}
