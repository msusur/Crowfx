using System.Collections.Generic;

namespace Crow.Library.CodeExecutionFlow
{
    public class MachineFlowDefinitionCollection : List<MachineFlowDefinition>
    {
        public MachineFlowDefinitionCollection()
            : base()
        { }
        public MachineFlowDefinitionCollection(IEnumerable<MachineFlowDefinition> collection)
            : base()
        { }
        public MachineFlowDefinitionCollection(int capacity)
            : base()
        { }
    }
}
