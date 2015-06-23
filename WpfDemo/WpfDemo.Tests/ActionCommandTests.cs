using System;
using System.Linq.Expressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfDemo.Library;

namespace WpfDemo.Tests
{
    [TestClass]
    public class ActionCommandTests
    {
        [TestMethod]
        [ExpectedException(typeof (ArgumentNullException))]
        public void ConstructorThrowsExceptionIfActionParameterIsNull()
        {
            var command = new ActionCommand(null);
        }

        [TestMethod]
        public void ExecuteInvokesAction()
        {
            var invoked = false;
            Action<Object> action = obj => invoked = true;

            var command = new ActionCommand(action);
            command.Execute();

            Assert.IsTrue(invoked);
        }

        [TestMethod]
        public void ExecuteOverloadInvokesActionWithParameter()
        {
            var invoked = false;
            Action<Object> action = obj =>
            {
                Assert.IsNotNull(obj);
                invoked = true;
            };

            var command = new ActionCommand(action);
            command.Execute(new object());

            Assert.IsTrue(invoked);
        }

        [TestMethod]
        public void CanExecuteIsTrueByDefault()
        {
            var command = new ActionCommand(obj => { });
            Assert.IsTrue(command.CanExecute(null));
        }

        [TestMethod]
        public void CanExecuteOverloadInvokesActionExecutesTruePredicate()
        {
            var command = new ActionCommand(obj => { }, p => (int)p == 1);
            Assert.IsTrue(command.CanExecute(1));
        }
    
        [TestMethod]
        public void CanExecuteOverloadInvokesActionExecutesFalsePredicate()
        {
            var command = new ActionCommand(obj => { }, p => (int)p == 1);
            Assert.IsFalse(command.CanExecute(0));
        }

    }
}