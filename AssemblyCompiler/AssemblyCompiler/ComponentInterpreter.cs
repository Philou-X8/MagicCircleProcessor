using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssemblyCompiler
{
    internal class ComponentInterpreter
    {
        private string str = "";
        private StateComponent _state;

        public void ChangeState(StateComponent nextState)
        {
            _state = nextState;
        }
    }

    // ----------------------------------------------------------------------------------------------

    abstract class StateComponent
    {
        protected ComponentInterpreter _context;
        protected string _compStr;
        public StateComponent(ComponentInterpreter context, string ComponentStr) {
            _context = context;
            _compStr = ComponentStr;
        }
        public abstract void AddChar(char newChar);
        public abstract void getComponent();
    }

    // ----------------------------------------------------------------------------------------------

    class StateDefault : StateComponent
    {
        public StateDefault(ComponentInterpreter context, string ComponentStr) : base(context, ComponentStr) { }

        public override void AddChar(char newChar)
        {
            if (newChar == 'r')
            {
                _compStr = newChar.ToString();
                // _context.ChangeState(new StateRegister(_context, _compStr)); // TODO StateRegister
            }
            else if (newChar == '_')
            {
                _compStr = newChar.ToString();
                // _context.ChangeState(new StateRegister(_context, _compStr)); // TODO StateLabel
            }
            else if (newChar >= 'a' && newChar <= 'z')
            {
                _compStr = newChar.ToString();
                _context.ChangeState(new StateInstrucion(_context, _compStr));
            } 
            else if (newChar == ' ' ||  newChar == '\t')
            {
                // wait for caracter
            }
            else
            {
                // error
            }
        }
        public override void getComponent()
        {

        }

    }

    class StateInstrucion : StateComponent
    {
        public StateInstrucion(ComponentInterpreter context, string ComponentStr) : base(context, ComponentStr) { }

        public override void AddChar(char newChar)
        {
            if (newChar >= 'a' && newChar <= 'z')
            {
                _compStr += newChar;
                if (_compStr.Length == 3) // instruction over
                {
                    _context.ChangeState(new StateLabel(_context, _compStr)); // TODO: put right state
                }
                else if (_compStr.Length > 3) // instruction too long
                {
                    // invalid input, too long
                }


            }
            else if (newChar == ' ' || newChar == '\t')
            {
                if(_compStr.Length <= 3)
                {
                    // instruction 
                }
            }
            else
            {
                // error
            }
        }
        public override void getComponent()
        {

        }

    }

    class StateLabel : StateComponent
    {
        public StateLabel(ComponentInterpreter context, string ComponentStr) : base(context, ComponentStr) { }

        public override void AddChar(char newChar)
        {
            if (newChar >= 'a' && newChar <= 'z')
            {
                _compStr += newChar;
            }
            else if (newChar == ':')
            {
                _context.ChangeState(new StateDefault(_context, _compStr)); // TODO: set proper state (instruction)
            }
            else
            {
                // error
            }
        }
        public override void getComponent()
        {

        }

    }



}
