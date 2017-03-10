using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using biz.dfch.CS.Commons.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Net.Appclusive.Public.Constants;
using Net.Appclusive.Public.Engine;

namespace Net.Appclusive.Public.Tests.Engine
{
    [TestClass]
    public class StateMachineBuilderTest
    {
        public class MyProduct : BaseModel
        {
            
        }
            
        public class MyProductWithStringDefinition : MyProduct
        {
            private static readonly Lazy<StateMachine> _stateMachine = new Lazy<StateMachine>(() => 
                StateMachineBuilder.For<MyProductWithStringDefinition>()
                    .InsertAfter(nameof(Initialise), nameof(States.State1), nameof(ActionA))
                    .InsertAfter(nameof(ActionA), nameof(States.State2), nameof(ActionB))
                    .InsertAction(nameof(States.State1), nameof(ActionC), nameof(States.State2))
                    .InsertAction(nameof(States.State2), nameof(ActionD), nameof(States.State2))
                    .GetStateMachine()
            );

            [SuppressMessage("ReSharper", "UnassignedReadonlyField")]
            protected new class States : MyProduct.States
            {
                public static readonly ModelState State1;
                public static readonly ModelState State2;
            }

            public override StateMachine GetStateMachine()
            {
                return _stateMachine.Value;
            }

            public class ActionA : BaseModelAction
            {
                
            }

            public class ActionB : BaseModelAction
            {
                
            }

            public class ActionC : BaseModelAction
            {
                
            }

            public class ActionD : BaseModelAction
            {
                
            }
        }
            
        public class MyProductWithTypeDefinition : MyProductWithStringDefinition
        {
            private static readonly Lazy<StateMachine> _stateMachine = new Lazy<StateMachine>(() => 
                StateMachineBuilder.For<MyProduct>()
                    .InsertAfter(typeof(Initialise), () => States.State1, typeof(ActionA))
                    .InsertAfter(typeof(ActionA), () => States.State2, typeof(ActionB))
                    .InsertAction(() => States.State1, typeof(ActionC), () => States.State2)
                    .InsertAction(() => States.State2, typeof(ActionD), () => States.State2)
                    .GetStateMachine()
            );

            public override StateMachine GetStateMachine()
            {
                return _stateMachine.Value;
            }
        }
            
        [TestMethod]
        public void StringBuilderCreatingNodeTypeBaseStateMachineSucceeds()
        {
            var sut = StateMachineBuilder
                .For<BaseModel>()
                .GetStateMachine();

            Assert.IsTrue(sut.States.Contains(nameof(BaseModelStates.InitialState)));
            Assert.IsTrue(sut.States.Contains(nameof(BaseModelStates.DecommissionedState)));
            Assert.IsTrue(sut.States.Contains(nameof(BaseModelStates.FinalState)));
            Assert.IsTrue(sut.Actions.Contains(nameof(BaseModel.Initialise)));
            Assert.IsTrue(sut.Actions.Contains(nameof(BaseModel.Finalise)));

            var isValid = sut.IsValid();
            if(!isValid)
            {
                foreach (var errorMessage in sut.GetErrorMessages())
                {
                    Logger.Get(Logging.TraceSourceName.ENGINE).TraceInformation(errorMessage);
                }
            }
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void StringBuilderCreatingMyProductStateMachineSucceeds()
        {
            var sut = StateMachineBuilder
                .For<MyProduct>()
                .GetStateMachine();

            Assert.IsTrue(sut.States.Contains(nameof(BaseModelStates.InitialState)));
            Assert.IsTrue(sut.States.Contains(nameof(BaseModelStates.DecommissionedState)));
            Assert.IsTrue(sut.States.Contains(nameof(BaseModelStates.FinalState)));
            Assert.IsTrue(sut.Actions.Contains(nameof(BaseModel.Initialise)));
            Assert.IsTrue(sut.Actions.Contains(nameof(BaseModel.Finalise)));

            var isValid = sut.IsValid();
            if(!isValid)
            {
                foreach (var errorMessage in sut.GetErrorMessages())
                {
                    Logger.Get(Logging.TraceSourceName.ENGINE).TraceInformation(errorMessage);
                }
            }
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void StringBuilderCreatingMyProductWithStringDefinitionSucceeds()
        {
            var sut = new MyProductWithStringDefinition()
                .GetStateMachine();

            Trace.WriteLine(sut.ToString());

            Assert.IsTrue(sut.States.Contains(nameof(BaseModelStates.InitialState)));
            Assert.IsTrue(sut.States.Contains(nameof(BaseModelStates.DecommissionedState)));
            Assert.IsTrue(sut.States.Contains(nameof(BaseModelStates.InitialState)));
            Assert.IsTrue(sut.States.Contains("State1"));
            Assert.IsTrue(sut.States.Contains("State2"));
            Assert.IsTrue(sut.Actions.Contains(nameof(BaseModel.Initialise)));
            Assert.IsTrue(sut.Actions.Contains(nameof(BaseModel.Finalise)));
            Assert.IsTrue(sut.Actions.Contains(nameof(MyProductWithStringDefinition.ActionA)));
            Assert.IsTrue(sut.Actions.Contains(nameof(MyProductWithStringDefinition.ActionB)));
            Assert.IsTrue(sut.Actions.Contains(nameof(MyProductWithStringDefinition.ActionC)));
            Assert.IsTrue(sut.Actions.Contains(nameof(MyProductWithStringDefinition.ActionD)));

            var isValid = sut.IsValid();
            if(!isValid)
            {
                foreach (var errorMessage in sut.GetErrorMessages())
                {
                    Logger.Get(Logging.TraceSourceName.ENGINE).TraceInformation(errorMessage);
                }
            }
            Assert.IsTrue(isValid);
        }

        [TestMethod]
        public void StringBuilderCreatingMyProductTypeSucceeds()
        {
            var sut = StateMachineBuilder
                .For<MyProductWithTypeDefinition>()
                .GetStateMachine();

            Trace.WriteLine(sut.ToString());

            Assert.IsTrue(sut.States.Contains(nameof(BaseModelStates.InitialState)));
            Assert.IsTrue(sut.States.Contains(nameof(BaseModelStates.DecommissionedState)));
            Assert.IsTrue(sut.States.Contains(nameof(BaseModelStates.InitialState)));
            Assert.IsTrue(sut.States.Contains("State1"));
            Assert.IsTrue(sut.States.Contains("State2"));
            Assert.IsTrue(sut.Actions.Contains(nameof(BaseModel.Initialise)));
            Assert.IsTrue(sut.Actions.Contains(nameof(BaseModel.Finalise)));
            Assert.IsTrue(sut.Actions.Contains(nameof(MyProductWithStringDefinition.ActionA)));
            Assert.IsTrue(sut.Actions.Contains(nameof(MyProductWithStringDefinition.ActionB)));
            Assert.IsTrue(sut.Actions.Contains(nameof(MyProductWithStringDefinition.ActionC)));
            Assert.IsTrue(sut.Actions.Contains(nameof(MyProductWithStringDefinition.ActionD)));

            var isValid = sut.IsValid();
            if(!isValid)
            {
                foreach (var errorMessage in sut.GetErrorMessages())
                {
                    Logger.Get(Logging.TraceSourceName.ENGINE).TraceInformation(errorMessage);
                }
            }
            Assert.IsTrue(isValid);
        }
    }
}
