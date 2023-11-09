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

using Cofo.Modules.EmailRegistrationWall.Components;
using Cofo.Modules.EmailRegistrationWall.Models;
using DotNetNuke.Collections;
using DotNetNuke.Entities.Users;
using DotNetNuke.Framework.JavaScriptLibraries;
using DotNetNuke.UI.Containers;
using DotNetNuke.Web.Mvc.Framework.ActionFilters;
using DotNetNuke.Web.Mvc.Framework.Controllers;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Cofo.Modules.EmailRegistrationWall.Controllers
{
    [DnnHandleError]
    public class ItemController : DnnController
    {

        public ActionResult Submit(string email)
        {
            // if email is not in the database, add it
            var items = ItemManager.Instance.GetItems(ModuleContext.ModuleId);
            var item = items.Where(i => i.EmailAddress == email).FirstOrDefault();
            if (item == null)
            {
                item = new Item
                {
                    EmailAddress = email,
                    Event = ModuleContext.Configuration.ModuleSettings.GetValueOrDefault("EventName", ""),
                    ModuleId = ModuleContext.ModuleId
                };
                ItemManager.Instance.CreateItem(item);
            }
            
            var signin = new SignIns
            {
                ModuleId = ModuleContext.ModuleId,
                Time = DateTime.Now,
                UserId = item.Id
            };
            SignInsManager.Instance.CreateSignIns(signin);

            return Json(new { success = true, message = "Success" });
        }

        [ModuleAction(ControlKey = "Edit", TitleKey = "AddItem")]
        public ActionResult Index()
        {
            var items = ItemManager.Instance.GetItems(ModuleContext.ModuleId);
            return View(items);
        }
    }
}
