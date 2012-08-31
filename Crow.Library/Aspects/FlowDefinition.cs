using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Crow.Library.CodeExecutionFlow
{
    public class MachineFlowDefinition
    {
        public string FlowCode { get; set; }
        public List<MachineWorkStep> Steps { get; set; }
    }
}
