using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace TestApp {
    public partial class MainWindow : Window {
        public int LeftFileOrRight= 0 ;
        bool flag = false;
        XmlDocument LeftFileXml,RightFileXml;
        public MainWindow() {

            InitializeComponent();
        }
        private void Left_Click(object sender, RoutedEventArgs e) {
            string file = OpenFile();
            if (file != null) {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(file);
                LeftFile.Items.Clear();
                XmlElement xRoot = xDoc.DocumentElement;
                XmlDocument LeftFileXml; 
                Recursion(xRoot, 1,1);
            }
        }
        private void Right_Click(object sender, RoutedEventArgs e) {
            string file = OpenFile();
            List<string> l = new List<string>();
            if (file != null) {
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(file);
                RightFile.Items.Clear();
                XmlElement xRoot = xDoc.DocumentElement;
                Recursion(xRoot, 0,1);
            }
        }

        private void Run_Click(object sender, RoutedEventArgs e) {
            
            int count = RightFile.Items.Count;

            if (count>LeftFile.Items.Count) {
                count = LeftFile.Items.Count;
            }
            for (var i = 0; i < count; i++) {
                string r = (string)RightFile.Items[i];
                string l = (string)LeftFile.Items[i];
                if (r.Equals(l)&&r==l) {
                    MessageBox.Show("True");
                }
                else if(RightFile.Items[i]==LeftFile.Items[i]){
                    MessageBox.Show("True");
                } 
                else {
                    
                    MessageBox.Show("False");
                }
            }
            if (flag == true) {
                for (var i = 0; i < LeftFile.Items.Count;i++) {
                    RightFile.Items.Add(LeftFile.Items[i]);
                }
                for (var i = 0; i < RightFile.Items.Count; i++) {
                    LeftFile.Items.Add(RightFile.Items[i]);
                }
            }

        }


        int n = 1;
        void Recursion(XmlNode el, int LeftFileOrRight,int num) {
            if (el.HasChildNodes) {
                if (LeftFileOrRight == 0&&el.Name!="#text") {
                    RightFile.Items.Add(num+" "+el.Name+ " "+ el.Value);
                } else if (LeftFileOrRight == 1&&el.Name != "#text") {
                    LeftFile.Items.Add(num + " " + el.Name + " " + el.Value);
                }
                n++;
                foreach (XmlNode e in el) {
                    Recursion(e, LeftFileOrRight,n);
                }
                n--;
            } else {
                if (LeftFileOrRight == 0&& el.Name != "#text") {
                    RightFile.Items.Add(num + " " + el.Name + " " + el.Value);
                }else if (LeftFileOrRight == 1&& el.Name != "#text") {
                    LeftFile.Items.Add(num + " " + el.Name + " " + el.Value);
                }
            }
        }

        private void boolCompare_Checked(object sender, RoutedEventArgs e) {
            if (boolCompare.IsChecked == true ) {
                flag = true;
            }
        }

        public string OpenFile() {
            OpenFileDialog OPF = new OpenFileDialog();
            OPF.Filter = "Файлы xml|*.xml";
            OPF.ShowDialog();
            return OPF.FileName;
        }
    }
}
