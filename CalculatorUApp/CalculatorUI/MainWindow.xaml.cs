using CalculatorLib;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculatorUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields and Properties

        public CalcLogic Logic { get; set; } = new CalcLogic();

        #endregion

        #region Methods

        public MainWindow()
        {
            InitializeComponent();
            Reset();
        }

        private void Reset()
        {
            Logic.Reset();
            txt_Value.Text = string.Empty;
            txt_Memory.Text = string.Empty;
            SetValueText();
        }

        private void Type(string value)
        {
            Logic.Type(value);
            SetValueText();
        }

        private void Clear()
        {
            Logic.Clear();
            SetValueText();
        }

        private void Negate()
        {
            Logic.Negate();
            SetValueText();
        }

        private void DoOperation(Operation operation)
        {
            Logic.DoOperation(operation, txt_Value.Text);
            SetValueText();
        }

        private void Calculate()
        {
            Logic.Calculate(txt_Value.Text);
            SetValueText();
        }

        private void SetValueText()
        {
            txt_Value.Text = $@"{Logic.Number1}{Constants.OperationString[(int)Logic.CurrentOperation]}{Logic.Number2}";
            lst_History.ItemsSource = Logic.History;
            txt_Memory.Text = $@"{Logic.MemoryValue}";
        }

        private void MemoryClear()
        {
            Logic.MemoryClear();
            SetValueText();
        }

        private void MemoryRecall()
        {
            Logic.MemoryRecall();
            SetValueText();
        }

        private void MemoryPlus()
        {
            Logic.MemoryPlus();
            SetValueText();
        }

        private void MemoryMinus()
        {
            Logic.MemoryMinus();
            SetValueText();
        }

        private void Exit()
        {
            Application.Current.Shutdown();
        }

        private void Copy()
        {
            Logic.Copy();
            SetValueText();
        }

        private void Paste()
        {
            Logic.Paste();
            SetValueText();
        }

        private void ExportToXml()
        {
            SaveFileDialog dialog = new SaveFileDialog();
            if (dialog.ShowDialog() == true)
                Logic.ExportToXml(dialog.FileName);

            SetValueText();
        }

        private void ImportFromXml()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
                Logic.ImportFromXml(dialog.FileName);

            SetValueText();
        }

        #endregion

        #region Auto-generated Methods

        #region Numbers and Period

        private void btn_1_Click(object sender, RoutedEventArgs e)
        {
            Type("1");
        }

        private void btn_2_Click(object sender, RoutedEventArgs e)
        {
            Type("2");
        }

        private void btn_3_Click(object sender, RoutedEventArgs e)
        {
            Type("3");
        }

        private void btn_4_Click(object sender, RoutedEventArgs e)
        {
            Type("4");
        }

        private void btn_5_Click(object sender, RoutedEventArgs e)
        {
            Type("5");
        }

        private void btn_6_Click(object sender, RoutedEventArgs e)
        {
            Type("6");
        }

        private void btn_7_Click(object sender, RoutedEventArgs e)
        {
            Type("7");
        }

        private void btn_8_Click(object sender, RoutedEventArgs e)
        {
            Type("8");
        }

        private void btn_9_Click(object sender, RoutedEventArgs e)
        {
            Type("9");
        }

        private void btn_0_Click(object sender, RoutedEventArgs e)
        {
            Type("0");
        }

        private void btn_Period_Click(object sender, RoutedEventArgs e)
        {
            Type(".");
        }

        #endregion

        #region Memory

        private void btn_MC_Click(object sender, RoutedEventArgs e)
        {
            MemoryClear();
        }

        private void btn_MR_Click(object sender, RoutedEventArgs e)
        {
            MemoryRecall();
        }

        private void btn_MPlus_Click(object sender, RoutedEventArgs e)
        {
            MemoryPlus();
        }

        private void btn_MMinus_Click(object sender, RoutedEventArgs e)
        {
            MemoryMinus();
        }

        #endregion

        #region Operations

        private void btn_Clear_Click(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void btn_Negate_Click(object sender, RoutedEventArgs e)
        {
            Negate();
        }

        private void btn_Divide_Click(object sender, RoutedEventArgs e)
        {
            DoOperation(Operation.Divide);
        }

        private void btn_Multiply_Click(object sender, RoutedEventArgs e)
        {
            DoOperation(Operation.Multiply);
        }

        private void btn_Minus_Click(object sender, RoutedEventArgs e)
        {
            DoOperation(Operation.Subtract);
        }

        private void btn_Plus_Click(object sender, RoutedEventArgs e)
        {
            DoOperation(Operation.Add);
        }

        private void btn_Equals_Click(object sender, RoutedEventArgs e)
        {
            Calculate();
        }

        #endregion

        #region Menu

        private void menu_Reset_Click(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void menu_Exit_Click(object sender, RoutedEventArgs e)
        {
            Exit();
        }

        private void menu_Copy_Click(object sender, RoutedEventArgs e)
        {
            Copy();
        }

        private void menu_Paste_Click(object sender, RoutedEventArgs e)
        {
            Paste();
        }

        private void Menu_ExportXml_Click(object sender, RoutedEventArgs e)
        {
            ExportToXml();
        }

        private void Menu_ImportXml_Click(object sender, RoutedEventArgs e)
        {
            ImportFromXml();
        }

        #endregion

        #endregion
    }
}
