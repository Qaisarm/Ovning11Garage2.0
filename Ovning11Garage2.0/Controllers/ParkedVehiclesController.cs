using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Ovning11Garage2._0.Data;
using Ovning11Garage2._0.Models;
using Ovning11Garage2._0.Models.ViewModels;

namespace Ovning11Garage2._0.Controllers
{
    public class ParkedVehiclesController : Controller
    {
        private readonly Ovning11Garage2_0Context _context;

      

        public ParkedVehiclesController(Ovning11Garage2_0Context context)
        {
            _context = context;
        }

        // GET: ParkedVehicles
        public async Task<IActionResult> Index()
        {
            return View(await _context.ParkedVehicle.ToListAsync());
        }

        /* Search Based on Registration Number and Vehicle Type*/
        public async Task<IActionResult> Filter(string registrationNumber, int? vehicleType)
        {

           var model = string.IsNullOrWhiteSpace(registrationNumber) ?
                  _context.ParkedVehicle :
                   _context.ParkedVehicle.Where(rn => rn.RegistrationNumber
                                .Contains(registrationNumber));
            model = vehicleType == null ?
                model :
                model.Where(m => m.VehicleType == (VehicleType)vehicleType);
            return View(nameof(Index), await model.ToListAsync());
        }

        /* Search Based on Registration Number and Vehicle Type*/
        public async Task<IActionResult> Filter1(string registrationNumber, int? vehicleType)
        {

            var model = string.IsNullOrWhiteSpace(registrationNumber) ?
                   _context.ParkedVehicle :
                    _context.ParkedVehicle.Where(rn => rn.RegistrationNumber
                                 .Contains(registrationNumber));
            model = vehicleType == null ?
                model :
                model.Where(m => m.VehicleType == (VehicleType)vehicleType);
            var viewmodel = model.Select(v => new PVOverview2()
            {
                VehicleType = v.VehicleType,
                RegistrationNumber = v.RegistrationNumber,
                NumberOfWheels = v.NumberOfWheels,

            });


            return View(nameof(Overview1), await viewmodel.ToListAsync());
        }

        // Reciept Genration

        public async Task<IActionResult> Receipt(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle.FirstOrDefaultAsync(m => m.Id == id);

            if (parkedVehicle == null)
            {
                return NotFound();
            }

            var model = new ReceiptViewModel
            {
                RegistrationNumber = parkedVehicle.RegistrationNumber,
                VehicleType = parkedVehicle.VehicleType,
                TimeOfParking = parkedVehicle.TimeOfParking
            };
            
            model.TimeOfCheckOut = DateTime.Now;
            var parkingFee = 15;
            var totalTime = model.TimeOfCheckOut - model.TimeOfParking;
            var timeInMinuets = (totalTime.Minutes > 0) ? 1 : 0;


            if (totalTime.Days == 0)
            {
                model.TotalTime = totalTime.Hours + " Hours " + totalTime.Minutes + " " + " Minutes";
                model.TotalPrice = ((totalTime.Hours + timeInMinuets) * parkingFee) + " " + "SEK";
            }
            else
            {
                model.TotalTime = totalTime.Days + "Days" + " " + totalTime.Hours + "Hours" + " " + totalTime.Minutes + "Minuets";
                model.TotalPrice = (totalTime.Days * parkingFee* 10) + ((totalTime.Hours + timeInMinuets) * parkingFee) + " " + "SEK";
            }

            _context.ParkedVehicle.Remove(parkedVehicle);
            await _context.SaveChangesAsync(); //save the remove action 

            return View(model);
        }

        // GET: ParkedVehicles Overview
        public async Task<IActionResult> Overview()
        {
            var parkedVehicle = await _context.ParkedVehicle.ToListAsync();
            var model = parkedVehicle.Select(v => new ParkedVehicleViewModel() {
                VehicleType = v.VehicleType,
                RegistrationNumber = v.RegistrationNumber,
                Color = v.Color,
                TimeOfParking = v.TimeOfParking 
            }).ToList(); 
                 
            return View(model);
        }

        //overview for other attribute
        public async Task<IActionResult> Overview1()
        {
            var parkedVehicle = await _context.ParkedVehicle.ToListAsync();
            var model = parkedVehicle.Select(v => new PVOverview2()
            {
                VehicleType = v.VehicleType,
                RegistrationNumber = v.RegistrationNumber,
                NumberOfWheels=v.NumberOfWheels
               
            }).ToList();

            return View(model);
        }

        // GET: ParkedVehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ParkedVehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VehicleType,RegistrationNumber,Color,Brand,Model,TimeOfParking,NumberOfWheels")] ParkedVehicle parkedVehicle)
        {
            if (ModelState.IsValid)
            {
                parkedVehicle.TimeOfParking = DateTime.Now;

                // Check whether the Vehicle with same Registration Number is Parked or not
                var findRegistrationNr = _context.ParkedVehicle
                    .Where(rn => rn.RegistrationNumber == parkedVehicle.RegistrationNumber).ToList();
                if (findRegistrationNr.Count == 0)
                {
                    _context.Add(parkedVehicle);
                }
                else
                {
                    ModelState.AddModelError("RegistrationNumber", "Vehicle with same Registration Number is already Parked");
                    return View();
                
                        }
                
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);

            if (parkedVehicle == null)
            {
                return NotFound();
            }
            
            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,VehicleType,RegistrationNumber,Color,Brand,Model,NumberOfWheels")] ParkedVehicle parkedVehicle)
        {
            // Do not Update Time Of Parking with change in other properties of a Vehicle
            if (id != parkedVehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                
                try
                {
                    var editPostData = await _context.ParkedVehicle.FindAsync(id);
                    if (editPostData == null)
                    {
                        return NotFound();

                    }
                    else
                    {
                        editPostData.VehicleType = parkedVehicle.VehicleType;
                        editPostData.RegistrationNumber = parkedVehicle.RegistrationNumber;
                        editPostData.Color = parkedVehicle.Color;
                        editPostData.Brand = parkedVehicle.Brand;
                        editPostData.Model = parkedVehicle.Model;
                        editPostData.NumberOfWheels = parkedVehicle.NumberOfWheels;

                        await _context.SaveChangesAsync();
                    }
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkedVehicleExists(parkedVehicle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);

           // _context.ParkedVehicle.Remove(parkedVehicle);

            //await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool ParkedVehicleExists(int id)
        {
            return _context.ParkedVehicle.Any(e => e.Id == id);
        }
    }
}
