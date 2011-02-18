using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nuxeo.Otg.Win
{
    public class ActionsDispatcher
    {
        protected static Dictionary<String, List<String>> actionsByStates =
            new Dictionary<String, List<String>>();

        protected String state;

        static ActionsDispatcher()
        {
            List<String> actions = new List<String>();
            actions.Add(Constants.ACTION_SYNCHRONZATION);

            actionsByStates[Constants.STATE_UNKNOWN] = actions;
            actionsByStates[Constants.STATE_UP_TO_DATE] = actions;
            actionsByStates[Constants.STATE_WORK_IN_PROGRESS] = actions;
            actionsByStates[Constants.STATE_UNATTACHED] = actions;


            actions = new List<String>();
            actions.Add(Constants.ACTION_SYNCHRONZATION);
            actions.Add(Constants.ACTION_REFRESH);

            actionsByStates[Constants.STATE_LOCALY_MODIFIED] = actions;
            actionsByStates[Constants.STATE_REMOTELY_MODIFIED] = actions;


            actions = new List<String>();
            actions.Add(Constants.ACTION_BIND);
            actions.Add(Constants.ACTION_UNBIND);

            actionsByStates[Constants.STATE_DIRECTORY] = actions;
        }

        public ActionsDispatcher(String state)
        {
            this.state = state;
        }

        /// <summary>
        /// Current Actions property
        /// </summary>
        public List<String> CurrentActions
        {
            get
            {
                return actionsByStates[state];
            }
        }

        /// <summary>
        /// Execute the specified action
        /// </summary>
        /// <param name="action"></param>
        public void ExecuteAction(String action, String fileName)
        {
            switch (action)
            {
                case Constants.ACTION_BIND:
                    Forms.FrmBind frm = new Forms.FrmBind();
                    if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        CallClientExe(Constants.ACTION_BIND, fileName,
                            frm.RemoteFolder, frm.Url, frm.Username, frm.Password);
                    }
                    break;
                default:
                    break;
            }
        }

        protected void CallClientExe(String action, params String[] args)
        {
            System.Windows.Forms.MessageBox.Show("Action : " + action + 
                " with params : " + args.ToString());
        }
    }
}
