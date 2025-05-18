using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Cineplex_Management.Models;
using Microsoft.EntityFrameworkCore;
using Cineplex_Management.VM;
using static Cineplex_Management.VM.HallVm;

namespace Cineplex_Management.Controllers
{
    public class HallsController : Controller
    {
        CineplexContext _ctx;
        public HallsController(CineplexContext ctx)
        {
            _ctx = ctx;
        }
        // GET: HallsController
        public ActionResult Index()
        {
            return View(_ctx.Halls.ToList());
        }

        // GET: HallsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HallsController/Create
        public ActionResult Create()
        {
            return View(new Hall());
        }

        // POST: HallsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                var model = new Hall();
                model.HallName = collection["HallName"];
                model.Row = Convert.ToInt32(collection["Row"]);
                model.Column = Convert.ToInt32(collection["Column"]);
                _ctx.Halls.Add(model);
                _ctx.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HallsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_ctx.Halls.Where(x => x.HallId == id).FirstOrDefault());
        }

        // POST: HallsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                var model = _ctx.Halls.Where(x => x.HallId == id).FirstOrDefault();
                if (model != null)
                {
                    model.HallName = collection["HallName"];
                    model.Row = Convert.ToInt32(collection["Row"]);
                    model.Column = Convert.ToInt32(collection["Column"]);
                    _ctx.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HallsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_ctx.Halls.Where(x => x.HallId == id).FirstOrDefault());
        }
        // POST: HallsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                var model = _ctx.Halls.Where(x => x.HallId == id).FirstOrDefault();
                if (model != null)
                {
                    _ctx.Halls.Remove(model);
                    _ctx.SaveChanges();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HallsController/EditSit/5
        public ActionResult EditSit(int id)
        {
            HallVm oHallVm = new HallVm();
            var model = _ctx.Halls.Where(x => x.HallId == id).FirstOrDefault();
            if (model != null)
            {
                oHallVm.HallId = model.HallId;
                oHallVm.HallName = model.HallName;
                oHallVm.Row = model.Row;
                oHallVm.Column = model.Column;
                List<SeatPlanVm> listSeatPlanVm = new List<SeatPlanVm>();
                var listSeatPlan = _ctx.SeatPlans.Where(x => x.HallId == model.HallId).ToList();
                foreach (var seatPlan in listSeatPlan)
                {
                    SeatPlanVm oSeatPlanVm = new SeatPlanVm();
                    oSeatPlanVm.Price = seatPlan.Price;
                    oSeatPlanVm.SeatNumber = seatPlan.SeatNumber;
                    oSeatPlanVm.RowNumber = seatPlan.RowNumber;
                    oSeatPlanVm.HallId = model.HallId;

                    listSeatPlanVm.Add(oSeatPlanVm);
                }
                oHallVm.SeatPlans = listSeatPlanVm;
            }
            return View(oHallVm);
        }

        // POST: HallsController/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult EditSit(HallVm model)
        {
            try
            {
                var oHall = _ctx.Halls.Where(x => x.HallId == model.HallId).FirstOrDefault();
                if (oHall != null)
                {
                    oHall.Row = model.Row;
                    oHall.Column = model.Column;
                    _ctx.SaveChanges();

                    var listSeatPlanRem = _ctx.SeatPlans.Where(x => x.HallId == model.HallId).ToList();
                    if (listSeatPlanRem.Count > 0)
                    {
                        _ctx.SeatPlans.RemoveRange(listSeatPlanRem);
                        _ctx.SaveChanges();
                    }

                    List<SeatPlan> listSeatPlan = new List<SeatPlan>();
                    foreach (var seatPlan in model.SeatPlans)
                    {
                        SeatPlan oSeatPlanVm = new SeatPlan();
                        oSeatPlanVm.Price = seatPlan.Price;
                        oSeatPlanVm.SeatNumber = seatPlan.SeatNumber;
                        oSeatPlanVm.RowNumber = seatPlan.RowNumber;
                        oSeatPlanVm.HallId = model.HallId;

                        listSeatPlan.Add(oSeatPlanVm);
                    }
                    _ctx.SeatPlans.AddRange(listSeatPlan);
                    _ctx.SaveChanges();
                }
                //return RedirectToAction(nameof(Index));
                return Json(new { code = 200, message = "sucess" });
            }
            catch (Exception ex)
            {
                //return RedirectToAction(nameof(Index));
                return Json(new { code = 500, message = "fail" });
            }
        }

        // GET: HallsController/EditPrice/5
        public ActionResult EditPrice(int id)
        {
            HallVm oHallVm = new HallVm();
            var model = _ctx.Halls.Where(x => x.HallId == id).FirstOrDefault();
            if (model != null)
            {
                oHallVm.HallId = model.HallId;
                oHallVm.HallName = model.HallName;
                oHallVm.Row = model.Row;
                oHallVm.Column = model.Column;
                List<SeatPlanVm> listSeatPlanVm = new List<SeatPlanVm>();
                var listSeatPlan = _ctx.SeatPlans.Where(x => x.HallId == model.HallId).ToList();
                foreach (var seatPlan in listSeatPlan)
                {
                    SeatPlanVm oSeatPlanVm = new SeatPlanVm();
                    oSeatPlanVm.Price = seatPlan.Price;
                    oSeatPlanVm.SeatNumber = seatPlan.SeatNumber;
                    oSeatPlanVm.RowNumber = seatPlan.RowNumber;
                    oSeatPlanVm.HallId = model.HallId;

                    listSeatPlanVm.Add(oSeatPlanVm);
                }
                oHallVm.SeatPlans = listSeatPlanVm;
            }
            return View(oHallVm);
        }

        [HttpPost]
        public ActionResult EditPrice(HallVm model)
        {
            try
            {
                var oHall = _ctx.Halls.Where(x => x.HallId == model.HallId).FirstOrDefault();
                if (oHall != null)
                {
                    foreach (var rowPrice in model.SeatPlans)
                    {
                        var listRowPrice = _ctx.SeatPlans.Where(x => x.HallId == model.HallId && x.RowNumber == rowPrice.RowNumber).ToList();
                        foreach (var item in listRowPrice)
                        {
                            var oRowPrice = _ctx.SeatPlans.Where(x => x.SeatPlanId == item.SeatPlanId && x.HallId == item.HallId).FirstOrDefault();
                            if (oRowPrice != null)
                            {
                                oRowPrice.Price = rowPrice.Price;
                                _ctx.SaveChanges();
                            }
                        }
                    }
                    _ctx.SaveChanges();
                }
                //return RedirectToAction(nameof(Index));
                return Json(new { code = 200, message = "sucess" });
            }
            catch (Exception ex)
            {
                //return RedirectToAction(nameof(Index));
                return Json(new { code = 500, message = "fail" });
            }
        }

    }
}
