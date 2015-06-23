using System;
using System.Runtime.Remoting.Channels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfDemo.Library;

namespace WpfDemo.Tests
{
    [TestClass]
    public class ViewModelTest
    {
        [TestMethod]
        public void PropertyChangedEventHandlerIsRaised()
        {
            var obj = new StubViewModel();

            bool raised = false;

            obj.PropertyChanged += (sender, e) =>
            {
                Assert.IsTrue(e.PropertyName == "ChangedProperty");
                raised = true;
            };

            obj.ChangedProperty = "New Value";

            if (!raised) Assert.Fail("PropertyChanged was never invoked.");
        }

        class StubViewModel : ViewModel
        {
            private string _changedProperty;

            public string ChangedProperty
            {
                get { return _changedProperty; }
                set
                {
                    _changedProperty = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
