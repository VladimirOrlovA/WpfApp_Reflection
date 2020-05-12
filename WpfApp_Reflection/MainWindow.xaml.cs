using Microsoft.Win32;
using System;
using System.Reflection;
using System.Text;
using System.Windows;

namespace WpfApp_Reflection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            string filePath = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                filePath = openFileDialog.FileName;
            }

            tbxFilePath.Text = filePath;
            tbxViewInfo.Text = "";

            var currentAssembly = Assembly.LoadFrom(filePath);
            GetAssemblyInfo(currentAssembly);
            GetAssemblyReferenceInfo(currentAssembly);


            #region first try
            /*
            var resStr = new StringBuilder();
            foreach (var item in allReferencedAssemblies)
            {
                resStr.Append($"1. { item.Name} \n");

                var item2 = Assembly.Load(item).GetReferencedAssemblies();

                foreach (var item3 in item2)
                {
                    resStr.Append($"\t2. {item3.Name} \n");

                    var item4 = Assembly.Load(item3).GetReferencedAssemblies();

                    foreach (var item5 in item4)
                    {
                        resStr.Append($"\t\t3. {item5.Name} \n");

                        var item6 = Assembly.Load(item5).GetReferencedAssemblies();

                        foreach (var item7 in item6)
                        {
                            resStr.Append($"\t\t\t4. {item7.Name} \n");
                        }
                    }
                }


            }

            tbxViewInfo.Text = resStr.ToString();
            */
            #endregion
            //tbxViewInfo.Text = File.ReadAllText(openFileDialog.FileName);
        }

        void GetAssemblyInfo(Assembly currentAssembly)
        {
            Type[] types = currentAssembly.GetTypes();
            var strBuilder = new StringBuilder();
            foreach (var type in types)
            {
                strBuilder.Append($"{type.Name}\n");
            }
            tbxViewInfo.Text += strBuilder;
        }

        int cntLevel, cntRef = 0;
        
        void GetAssemblyReferenceInfo(Assembly currentAssembly)
        {
            StringBuilder str = new StringBuilder();
            var allReferencedAssemblies = currentAssembly.GetReferencedAssemblies();

            foreach (var item in allReferencedAssemblies)
            {
                str.Append($"\n{cntRef++}. { item.Name} \n");

                var assemblyNameNextLevel = Assembly.Load(item).GetReferencedAssemblies();
                if (assemblyNameNextLevel.Length != 0)
                {
                    cntLevel++;
                    foreach (var item1 in assemblyNameNextLevel)
                    {
                        str.Append($"\t{cntLevel++}. { item1.Name} \n");
                    }
                    cntLevel = 0;
                }
            }


            tbxViewInfo.Text += str.ToString();
        }


        //int count = 0;
        //private void GetAssembly(AssemblyName[] assemblyName)
        //{
        //    int cntRef = 0;

        //    var str = new StringBuilder();
        //    foreach (var item in assemblyName)
        //    {
        //        str.Append( $"\n\n{cntRef++}. { item.Name} \n");

        //        str.Append( $"Methods -> \n");
        //        var methods = item.GetType().GetMethods();
        //        int cntMethod = 0;
        //        foreach (var method in methods)
        //        {
        //            str.Append( $"\t{cntMethod++}. { method.Name} \n");

        //            str.Append($"Members -> \n");
        //            var members = method.GetType().GetMembers();
                    
        //              int cntMemb = 0;
        //            foreach (var member in members)
        //            {
        //                str.Append($"\t\t{cntMemb++}. { member.Name} \n");

        //                str.Append($"Properties -> \n");
        //                var properties = member.GetType().GetProperties();
        //                int cntProp = 0;
        //                foreach (var prop in properties)
        //                {
        //                    str.Append($"\t\t\t{cntProp++}. { prop.Name} - {prop.PropertyType.Name}\n");

        //                    //str.Append( $"Fields -> \n");
        //                    //var fields = item.GetType().GetFields();
        //                    //int cntField = 0;
        //                    //foreach (var field in fields)
        //                    //{
        //                    //    str.Append( $"\t{cntField++}. { field.Name} \n");
        //                    //}
        //                }
        //            }
        //        }
        //    }


        //    tbxViewInfo.Text = str.ToString();

        //}
    }
}
