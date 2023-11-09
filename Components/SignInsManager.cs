/*
' Copyright (c) 2023 Webmaster
'  All rights reserved.
' 
' THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED
' TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
' THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
' CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
' DEALINGS IN THE SOFTWARE.
' 
*/

using Cofo.Modules.EmailRegistrationWall.Models;
using DotNetNuke.Data;
using DotNetNuke.Framework;
using System.Collections.Generic;

namespace Cofo.Modules.EmailRegistrationWall.Components
{
    internal interface ISignInsManager
    {
        void CreateSignIns(SignIns t);
        void DeleteSignIns(int itemId, int moduleId);
        void DeleteSignIns(SignIns t);
        IEnumerable<SignIns> GetSignInss(int moduleId);
        SignIns GetSignIns(int itemId, int moduleId);
        void UpdateSignIns(SignIns t);
    }

    internal class SignInsManager : ServiceLocator<ISignInsManager, SignInsManager>, ISignInsManager
    {
        public void CreateSignIns(SignIns t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<SignIns>();
                rep.Insert(t);
            }
        }

        public void DeleteSignIns(int itemId, int moduleId)
        {
            var t = GetSignIns(itemId, moduleId);
            DeleteSignIns(t);
        }

        public void DeleteSignIns(SignIns t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<SignIns>();
                rep.Delete(t);
            }
        }

        public IEnumerable<SignIns> GetSignInss(int moduleId)
        {
            IEnumerable<SignIns> t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<SignIns>();
                t = rep.Get(moduleId);
            }
            return t;
        }

        public SignIns GetSignIns(int itemId, int moduleId)
        {
            SignIns t;
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<SignIns>();
                t = rep.GetById(itemId, moduleId);
            }
            return t;
        }

        public void UpdateSignIns(SignIns t)
        {
            using (IDataContext ctx = DataContext.Instance())
            {
                var rep = ctx.GetRepository<SignIns>();
                rep.Update(t);
            }
        }

        protected override System.Func<ISignInsManager> GetFactory()
        {
            return () => new SignInsManager();
        }
    }
}
