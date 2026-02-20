using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockManagement.Data;
using StockManagement.Models;

namespace StockManagement.Controllers
{
    public class StockController : Controller
    {
        private readonly AppDbContext _context;

        public StockController(AppDbContext context)
        {
            _context = context;
        }

        // GET: /Stock/ManagerDashboard
        public IActionResult ManagerDashboard()
        {
            if (HttpContext.Session.GetString("Role") != "manager")
                return RedirectToAction("Login", "Account");

            var orders = _context.Orders
                .Include(o => o.User)
                .Include(o => o.Item)
                .Where(o => !o.IsProcessed)
                .ToList();

            var items = _context.Items.ToList();

            ViewBag.Orders = orders;
            return View(items); // show stock list too
        }

        // GET: /Stock/UserDashboard
        public IActionResult UserDashboard()
        {
            if (HttpContext.Session.GetString("Role") != "user")
                return RedirectToAction("Login", "Account");

            var items = _context.Items.ToList();
            return View(items); // show available items
        }

        // POST: /Stock/PlaceOrder
        [HttpPost]
        public IActionResult PlaceOrder(int itemId, int quantity)
        {
            if (HttpContext.Session.GetString("Role") != "user")
                return RedirectToAction("Login", "Account");

            var username = HttpContext.Session.GetString("Username");
            var user = _context.Users.FirstOrDefault(u => u.Username == username);

            if (user == null) return RedirectToAction("Login", "Account");

            var order = new Order
            {
                UserId = user.Id,
                ItemId = itemId,
                Quantity = quantity,
                IsProcessed = false
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            return RedirectToAction("UserDashboard");
        }

        // POST: /Stock/AddItem
        [HttpPost]
        public IActionResult AddItem(string name, decimal price, int quantity)
        {
            if (HttpContext.Session.GetString("Role") != "manager")
                return RedirectToAction("Login", "Account");

            var item = new Item { Name = name, Price = price, Quantity = quantity };
            _context.Items.Add(item);
            _context.SaveChanges();

            return RedirectToAction("ManagerDashboard");
        }

        // POST: /Stock/DeleteItem
        [HttpPost]
        public IActionResult DeleteItem(int itemId)
        {
            if (HttpContext.Session.GetString("Role") != "manager")
                return RedirectToAction("Login", "Account");

            var item = _context.Items.Find(itemId);
            if (item != null)
            {
                _context.Items.Remove(item);
                _context.SaveChanges();
            }

            return RedirectToAction("ManagerDashboard");
        }

        // POST: /Stock/UpdateItem
        [HttpPost]
        public IActionResult UpdateItem(int id, string name, decimal price, int quantity)
        {
            if (HttpContext.Session.GetString("Role") != "manager")
                return RedirectToAction("Login", "Account");

            var item = _context.Items.Find(id);
            if (item != null)
            {
                item.Name = name;
                item.Price = price;
                item.Quantity = quantity;
                _context.SaveChanges();
            }

            return RedirectToAction("ManagerDashboard");
        }

        // POST: /Stock/MarkOrderProcessed
        [HttpPost]
        public IActionResult MarkOrderProcessed(int orderId)
        {
            if (HttpContext.Session.GetString("Role") != "manager")
                return RedirectToAction("Login", "Account");

            var order = _context.Orders.Find(orderId);
            if (order != null)
            {
                order.IsProcessed = true;
                _context.SaveChanges();
            }

            return RedirectToAction("ManagerDashboard");
        }
    }
}
