using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Reveal.Forms
{
    public class DocumentDockForm : DockForm
    {
        public virtual ToolStripMenuItem[] ToolItems { get { return null; } }
        public virtual ToolStrip[] Toolbars { get { return null; } }
        public virtual UIReflectController UIController { get { return null; } }

        public virtual void SaveThis(string dest) { }
        public virtual void UndoRedo(bool isRedo) { }
        public virtual object CutCopy(bool isCut) { return null; }
        public virtual void Paste(object paste) { }
        public virtual void Delete() { }
        public virtual void SelectAll() { }
        public string EditingFile { get; set; }

    }

    public class UIReflectController
    {
        public UIReflectController()
        {
            valueStorage = new Dictionary<UIElements, bool>();
        }

        public event EventHandler DesignChanged;
        private void DesignChangedInvoke()
        {
            if (DesignChanged != null)
                DesignChanged.Invoke(this, new EventArgs());
        }

        public enum UIElements
        {
            EditEnabled,
            ToolEnabled,
            Savable,
            Undoable,
            Redoable,
            Deletable,
            SelectAllCapable,
            CodeEditable,
            VisualEditable
        };

        Dictionary<UIElements, bool> valueStorage;

        public void SetEnabled(UIElements elem, bool value)
        {
            if (valueStorage.ContainsKey(elem))
                valueStorage[elem] = value;
            else
                valueStorage.Add(elem, value);
            DesignChangedInvoke();
        }

        public bool GetEnabled(UIElements elem)
        {
            if (valueStorage.ContainsKey(elem))
                return valueStorage[elem];
            else
                return false;
        }

        string statusText;
        public string StatusText
        {
            get { return this.statusText; }
            set
            {
                this.statusText = value;
                DesignChangedInvoke();
            }
        }

    }
}
