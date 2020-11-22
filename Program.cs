/*Задание 9 вариант 6 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab_9_6_csharp_
{
    class Element
    {

        private string name;
        private int numOfInput;
        private int numOfOutput;

        public Element()
        {
            name = "";
            numOfInput = 0;
            numOfInput = 0;

        }
        public Element(string Name, int NumOfInput = 2, int NumOfOutput = 1)
        {
            name = Name;
            if (NumOfInput >= 2) { numOfInput = NumOfInput; }
            else { numOfInput = 2; }
            if (NumOfOutput >= 1) { numOfOutput = NumOfOutput; }
            else { numOfOutput = 1; }
        }
        public string Name
        {
            get { return name; }
        }
        public int Input
        {
            get { return numOfInput; }
            set
            {
                if (value >= 1)
                {
                    numOfInput = value;
                }
            }
        }//Input
        public virtual int Output
        {
            get { return numOfOutput; }
            set
            {
                if (value >= 1)
                {
                    numOfOutput = value;
                }
            }
        }//Output

    }//class Element
    class Combinational : Element
    {
        private bool[] InputValues;

        public Combinational() : base("И-НЕ")
        {
            InputValues = null;
        }
        public Combinational(string Name) : base("И-НЕ")
        {
            InputValues = new bool[2];
            InputValues[0] = false;
            InputValues[1] = false;
        }
        public Combinational(string Name, int NumOfInput) : base("И-НЕ", NumOfInput)
        {
            if (NumOfInput >= 2)
            {
                InputValues = new bool[NumOfInput];
                for (int i = 0; i < NumOfInput; i++)
                {
                    InputValues[i] = false;
                }
            }
        }
        public bool this[int i]
        {
            get
            {
                return InputValues[i];
            }
            set
            {
                InputValues[i] = value;
            }
        }
        public override int Output
        {
            get
            {
                return base.Output;
            }
        }//Output
        public bool GetOutput()
        {
            bool Out = InputValues[0];
            for (int i = 1; i < this.Input; i++)
            {
                Out = InputValues[i] && Out;
            }
            return Out;
        }//GetOutput()
    }// class Combinational

    class Memory : Element
    {
        private bool[] InputValue;
        private bool q;
        private bool Nq;

        public Memory() : base("RS-Trigger", 2, 2)
        {
            InputValue = new bool[2];
            InputValue[0] = false;
            InputValue[1] = false;
            q = true;
            Nq = !q;
        }
        public Memory(Memory Copy) : this()
        {
            InputValue[0] = Copy.InputValue[0];
            InputValue[1] = Copy.InputValue[0];
            q = Copy.q;
            Nq = !q;
        }
        public bool this[char Input]
        {
            get
            {
                if (Input == 'R' || Input == 'r')
                {
                    return InputValue[0];
                }
                if (Input == 'S' || Input == 's')
                {
                    return InputValue[1];
                }
                return false;
            }
            set
            {
                if (Input == 'R' || Input == 'r')
                {
                    InputValue[0] = value;
                    if (value && InputValue[1])
                    {
                        InputValue[0] = false;
                    }
                    else if (value)
                    {
                        q = true;
                        Nq = false;
                    }
                }
                if (Input == 'S' || Input == 's')
                {
                    InputValue[1] = value;
                    if (value && InputValue[0])
                    {
                        InputValue[1] = false;
                    }
                    else if (value)
                    {
                        q = false;
                        Nq = true;
                    }
                }//this[char Input]
            }


        }
        public bool Q => q;
        public bool NQ => Nq;
        static public bool operator ==(Memory M1, Memory M2)
        {
            return M1.Q == M2.Q;
        }
        static public bool operator !=(Memory M1, Memory M2)
        {
            return M1.Q != M2.Q;
        }
        public override bool Equals(object obj)
        {
            return obj is Memory memory &&
                   Name == memory.Name &&
                   Input == memory.Input &&
                   Output == memory.Output &&
                   EqualityComparer<bool[]>.Default.Equals(InputValue, memory.InputValue) &&
                   q == memory.q &&
                   Nq == memory.Nq &&
                   Q == memory.Q &&
                   NQ == memory.NQ;
        }
        public override int GetHashCode()
        {
            int hashCode = 2003069013;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + Input.GetHashCode();
            hashCode = hashCode * -1521134295 + Output.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<bool[]>.Default.GetHashCode(InputValue);
            hashCode = hashCode * -1521134295 + q.GetHashCode();
            hashCode = hashCode * -1521134295 + Nq.GetHashCode();
            hashCode = hashCode * -1521134295 + Q.GetHashCode();
            hashCode = hashCode * -1521134295 + NQ.GetHashCode();
            return hashCode;
        }
    }
    class Registr : Element {
        private bool     s;
        private bool     r;
        private Memory[] rOutput;
        private bool[]   rInput;

        public Registr():base("Registr", 10, 10)
        {
            s = false;
            r = false;
            rOutput = new Memory[10];
            rInput  = new bool[10];
            for (int i = 0; i < 10; i++)
            {
                rOutput[i] = new Memory();
                rInput[i]  = false;
            }
        }
        public bool S {
            get { return s; }
            set
            {
                s = value;
                if (value) {
                    for (int i = 0; i < 10; i++)
                    {
                        rOutput[i]['r'] = rInput[i]; 
                    }
                }
            }
        }
        public bool R {
            get { return R; }
            set {
                r = value;
                if (value)
                {
                    for (int i = 0; i < 10; i++)
                    {
                        rOutput[i]['r'] = false;   
                    }
                }
            }
            }
    }
    class Program
    {
        static void Main(string[] args)
        {
        }
    }// class Program

}
