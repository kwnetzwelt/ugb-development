using UnityEngine;
using UnityEditor;
using NUnit.Framework;
using UnityGameBase.Core.Utils;
using System.Threading;

public class StateMachineTest {

	[Test]
	public void BasicStateMachineTest()
	{
        BaseStateMachine machine = new BaseStateMachine();
        Assert.IsNull( machine.GetActiveState());

        machine.AddState(new TestState("1"));
        machine.AddState(new TestState("2"));

        AutoResetEvent ase = new AutoResetEvent(false);

        machine.SetActiveState("1", () => {
            ase.Set();
        });

        bool triggered = ase.WaitOne(5000);
        Assert.True(triggered, "state change end callback not triggered" );
        
	}

    [Test]
    public void StateChangeCallbacksTest()
    {
        BaseStateMachine machine = new BaseStateMachine();
        machine.AddState(new TestState("1"));
        machine.AddState(new TestState("2"));

        AutoResetEvent ase = new AutoResetEvent(false);
        BaseState newStateResult = null;
        machine.StateChanged += (oldState, newState) => {
            newStateResult = newState;
            ase.Set();
        }; 

        BaseStateMachine.ResultCode stateChange = machine.SetActiveState("2", null);
        Assert.AreEqual(stateChange, BaseStateMachine.ResultCode.StateTransitionActivated, "");

        stateChange = machine.SetActiveState("1", null);
        Assert.AreEqual(stateChange, BaseStateMachine.ResultCode.StateTransitionActivated, "");

        bool triggered = ase.WaitOne(5000);
        Assert.True(triggered, "StateChanged callback not triggered");
        Assert.AreEqual("1", newStateResult.Name, "wrong new state was given in callback StateChanged");
    }

    public class TestState : BaseState
    {
        public TestState(string name) : base(name)
        {}

        public override void Start(System.Action onDone = null)
        {
            if(onDone != null)
                onDone();
        }
        public override void End(System.Action onDone = null)
        {

            if(onDone != null)
                onDone();
        }
        public override void Update()
        {

        }

    }
}
