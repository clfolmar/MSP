using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MSP.Models;
using MSP.Models.ViewModels;

namespace MSP.Controllers
{
    public class MessageSpecificationsController : Controller
    {
        // Session HashSet of Message Specifications, resets when program restarts.
        private HashSet<MessageSpecification> messageSpecifications = new HashSet<MessageSpecification>();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: MessageSpecifications
        public ActionResult Index()
        {
            return View(messageSpecifications);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        // GET: MessageSpecifications/Details/5
        public ActionResult Details(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageSpecification messageSpecification = messageSpecifications.FirstOrDefault(m => m.Name.Equals(name));

            ViewBag.MessageSpecificationOutput = Run(messageSpecification);

            if (messageSpecification == null)
            {
                return HttpNotFound();
            }
            return View(messageSpecification);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: MessageSpecifications/Create
        public ActionResult Create()
        {
            MessageSpecificationViewModel model = new MessageSpecificationViewModel();

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST: MessageSpecifications/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(/*[Bind(Include = "Name")]*/ MessageSpecificationViewModel model, List<string> Specification)
        {

            if (ModelState.IsValid && model.Specification.Any())
            {
                MessageSpecification ms = new MessageSpecification
                {
                    Name = model.Name
                };

                foreach (string node in model.Specification)
                {
                    // if the node is referencing another Message Specification
                    if (node.StartsWith("ms."))
                    {
                        string msName = node.Substring(3);

                        MessageSpecification referencedMessageSpecification = messageSpecifications.FirstOrDefault(m => m.Name.Equals(msName));

                        ms.Specification.Add(referencedMessageSpecification);
                    }
                    else
                    {
                        // otherwise it's a string literal, thus add it to the newly created Message Specification's Specification property
                        ms.Specification.Add(node);
                    }
                }

                messageSpecifications.Add(ms);

                return RedirectToAction("Index");
            }

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        // GET: MessageSpecifications/Edit/5
        public ActionResult Edit(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageSpecification messageSpecification = messageSpecifications.FirstOrDefault(m => m.Name.Equals(name));
            if (messageSpecification == null)
            {
                return HttpNotFound();
            }
            return View(messageSpecification);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // POST: MessageSpecifications/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit( )
        {
            if (ModelState.IsValid)
            {


                return RedirectToAction("Index");
            }
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        // GET: MessageSpecifications/Delete/5
        public ActionResult Delete(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MessageSpecification messageSpecification = messageSpecifications.FirstOrDefault(m => m.Name.Equals(name));
            if (messageSpecification == null)
            {
                return HttpNotFound();
            }
            return View(messageSpecification);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        // POST: MessageSpecifications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string name)
        {
            MessageSpecification messageSpecification = messageSpecifications.FirstOrDefault(m => m.Name.Equals(name));

            messageSpecifications.Remove(messageSpecification);

            return RedirectToAction("Index");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        // POST: MessageSpecifications/Run/5
        [HttpPost, ActionName("Run")]
        [ValidateAntiForgeryToken]
        public string Run(string name)
        {
            MessageSpecification messageSpecification = messageSpecifications.FirstOrDefault(m => m.Name.Equals(name));

            string output = "";

            foreach(Object node in messageSpecification.Specification)
            {
                if(node.GetType() == typeof(MessageSpecification))
                {
                    string result = Run((MessageSpecification)node);

                    string.Concat(output, result);
                }
                else
                {
                    string.Concat(output,(string)node);
                }
            }

            return output;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageSpecification"></param>
        /// <returns></returns>
        private string Run(MessageSpecification messageSpecification)
        {
            string output = "";

            foreach (Object node in messageSpecification.Specification)
            {
                if (node.GetType() == typeof(MessageSpecification))
                {
                   string result = Run((MessageSpecification)node);

                   string.Concat(output, result);
                }
                else
                {
                    string.Concat(output, (string)node);
                }
            }

            return output;
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        messageSpecifications.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
