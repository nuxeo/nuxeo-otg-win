using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nuxeo.Otg.Win
{
    public class Constants
    {
        public const String STATE_UNKNOWN = "unknown";
        public const String STATE_UP_TO_DATE = "up_to_date";
        public const String STATE_WORK_IN_PROGRESS = "work_in_progress";
        public const String STATE_LOCALY_MODIFIED = "locally_modified";
        public const String STATE_REMOTELY_MODIFIED = "remotely_modified";
        public const String STATE_UNATTACHED = "unattached";
        public const String STATE_DIRECTORY = "directory";

        public const String ACTION_STATUS = "status";
        public const String ACTION_REFRESH = "refresh";
        public const String ACTION_SYNCHRONZATION = "synchronization";
        public const String ACTION_BIND = "bind";
        public const String ACTION_UNBIND = "unbind";
        public const String ACTION_LIST_BINDINGS = "list_bindings";

        protected static Dictionary<String, String[]> labels = new Dictionary<string, string[]>();

        static Constants()
        {
            labels[ACTION_STATUS] = new String[] { "", "", "" };
            labels[ACTION_REFRESH] = new String[] { "Refresh", "Refresh selected file", "Help doc" };
            labels[ACTION_SYNCHRONZATION] = new String[] { "Synchronize", "Force Synchronize", "Help doc" };
            labels[ACTION_BIND] = new String[] { "Bind folder", "Bind the content of a local folder with a Nuxeo remote folder", "Help doc" };
            labels[ACTION_UNBIND] = new String[] { "Unbind folder", "Unbind the content of a local folder with a Nuxeo remote folder", "Help doc" };
            labels[ACTION_LIST_BINDINGS] = new String[] { "", "", "" };
        }

        public static String[] getLabels(String action)
        {
            return labels[action];
        }
    }
}
