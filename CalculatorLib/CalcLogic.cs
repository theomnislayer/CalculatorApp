using System.Collections.ObjectModel;
using System.Xml.Serialization;

namespace CalculatorLib
{
    public class CalcLogic
    {
        #region Fields and Properties

        public ObservableCollection<string> History { get; set; } = new ObservableCollection<string>();
        public string ValueHolder { get; set; } = string.Empty;
        public float? Number1 { get; set; } = null;
        public float? Number2 { get; set; } = null;
        public float? MemoryValue { get; set; } = 0;
        public float? Result { get; set; } = 0;
        public Operation CurrentOperation { get; set; } = Operation.None;

        public CalcLogic? CopyLogic { get; set; } = null;

        #endregion

        #region Methods

        #region Basic Functionality

        public void Reset()
        {
            History = new ObservableCollection<string>();
            ValueHolder = string.Empty;
            Number1 = null;
            Number2 = null;
            MemoryValue = 0;
            Result = 0;
            CurrentOperation = Operation.None;
            CopyLogic = null;
        }

        public void Type(string input)
        {
            if (input.Equals(".") && ValueHolder.Contains('.'))
            {
                //Do nothing
            }
            else if (ValueHolder.Length < Constants.LongestLength)
            {
                ValueHolder += input;
            }

            ParseNumber();
        }

        private void ParseNumber()
        {
            if (ValueHolder.Length > 0)
            {
                if (CurrentOperation == Operation.None)
                {
                    Number1 = float.Parse(ValueHolder);
                }
                else
                {
                    Number2 = float.Parse(ValueHolder);
                }
            }
        }

        public void Negate()
        {
            if (ValueHolder.StartsWith("-"))
            {
                ValueHolder = ValueHolder.Replace("-", string.Empty);
            }
            else if(ValueHolder.Length > 0)
            {
                ValueHolder = $@"-{ValueHolder}";
            }

            ParseNumber();
        }

        public void Clear()
        {
            CurrentOperation = Operation.None;
            ValueHolder = string.Empty;
            Number1 = null;
            Number2 = null;
        }

        public void DoOperation(Operation operation, string historyValue)
        {
            ValueHolder = string.Empty;
            CurrentOperation = operation;
            if(Number2 != null)
            {
                Calculate(historyValue);
            }
        }

        public void Calculate(string historyValue)
        {
            switch (CurrentOperation)
            {
                case Operation.Divide:
                    if (Number2 != 0)
                        Result = Number1 / Number2;
                    break;
                case Operation.Multiply:
                    Result = Number1 * Number2;
                    break;
                case Operation.Subtract:
                    Result = Number1 - Number2;
                    break;
                case Operation.Add:
                    Result = Number1 + Number2;
                    break;
                default:
                    if(!string.IsNullOrEmpty(ValueHolder))
                    {
                        Result = float.Parse(ValueHolder);
                    }
                    break;
            }
            History.Add($@"{historyValue} = {Result}");
            Number1 = Result;
            Number2 = null;
            CurrentOperation = Operation.None;
            ValueHolder = Result != null ? Result.ToString() : "0";
        }

        #endregion

        #region History

        public void HistoryClear()
        {
            History = new ObservableCollection<string>();
        }

        #endregion

        #region Memory

        public void MemoryClear()
        {
            HistoryClear();
            MemoryValue = 0;
        }

        public void MemoryPlus()
        {
            MemoryValue += Result;
        }

        public void MemoryMinus()
        {
            MemoryValue -= Result;
        }

        public void MemoryRecall()
        {
            Clear();
            Number1 = MemoryValue;
            Result = MemoryValue;
        }

        #endregion

        #region Menu

        public void Copy()
        {
            CopyLogic = new CalcLogic();
            CopyLogic.Number1 = Number1;
            CopyLogic.Number2 = Number2;
            CopyLogic.CurrentOperation = CurrentOperation;
        }

        public void Paste()
        {
            if(CopyLogic != null)
            {
                Clear();
                Number1 = CopyLogic.Number1;
                Number2 = CopyLogic.Number2;
                CurrentOperation = CopyLogic.CurrentOperation;
                Result = null;
            }
        }

        public void ExportToXml(string filePath)
        {
            try
            {
                XmlSerializer writer = new XmlSerializer(typeof(CalcLogic));
                if (File.Exists(filePath))
                    File.Delete(filePath);

                using (FileStream file = File.Create(filePath))
                {
                    writer.Serialize(file, this);
                }
            }
            catch
            {
                throw new Exception("Hey! Try to run as admin next time. - Glynn");
            }
        }

        public void ImportFromXml(string filePath)
        {
            if (File.Exists(filePath))
            {
                XmlSerializer reader = new XmlSerializer(typeof(CalcLogic));
                StreamReader streamReader = new StreamReader(filePath);
                try
                {
                    CalcLogic logicFromFile = (CalcLogic)reader.Deserialize(streamReader);
                    Reset();

                    History = logicFromFile.History;
                    ValueHolder = logicFromFile.ValueHolder;
                    Number1 = logicFromFile.Number1;
                    Number2 = logicFromFile.Number2;
                    MemoryValue = logicFromFile.MemoryValue;
                    Result = logicFromFile.Result;
                    CurrentOperation = logicFromFile.CurrentOperation;
                    CopyLogic = logicFromFile.CopyLogic;
                }
                catch
                {
                    throw new Exception("Try Running as admin or the file was changed. - Glynn");
                }
            }
        }

        #endregion

        #endregion
    }
}
