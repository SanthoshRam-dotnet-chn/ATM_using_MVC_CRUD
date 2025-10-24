using ATM_using_MVC_CRUD.Data;
using ATM_using_MVC_CRUD.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ATM_using_MVC_CRUD.Controllers
{
    public class ATMController : Controller
    {
        private readonly CustomerDbContext _custContext;
        private readonly AccountDbContext _accContext;

        public ATMController(CustomerDbContext custContext, AccountDbContext accContext)
        {
            _custContext = custContext;
            _accContext = accContext;
        }
        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> Login(int accountNumber, string pin)
        {
            var user = await _custContext.Customers
                .FirstOrDefaultAsync(u => u.AccountNumber == accountNumber && u.Pin == pin);

            if (user == null)
            {
                ViewBag.Error = "Invalid Account Number or PIN";
                return View();
            }

            TempData["AccountNumber"] = user.AccountNumber;
            return RedirectToAction("Dashboard");
        }
        public IActionResult Dashboard() => View();

        // ---------------- ATM Operations ----------------
        public IActionResult Deposit() => View();

        [HttpPost]
        public async Task<IActionResult> Deposit(decimal amount)
        {
            int accNo = Convert.ToInt32(TempData["AccountNumber"]);
            TempData.Keep();

            var trans = new AccountTransaction
            {
                AccountNumber = accNo,
                Amount = amount,
                Type = "Deposit"
            };
            _accContext.Transactions.Add(trans);
            await _accContext.SaveChangesAsync();

            ViewBag.Message = "Deposit Successful!";
            return View();
        }
        public IActionResult Withdraw() => View();

        [HttpPost]
        public async Task<IActionResult> Withdraw(decimal amount)
        {
            int accNo = Convert.ToInt32(TempData["AccountNumber"]);
            TempData.Keep();

            var balance = await _accContext.Transactions
                .Where(t => t.AccountNumber == accNo)
                .SumAsync(t => t.Type == "Deposit" ? t.Amount : -t.Amount);

            if (amount > balance)
            {
                ViewBag.Message = "Insufficient Balance!";
                return View();
            }
            var trans = new AccountTransaction
            {
                AccountNumber = accNo,
                Amount = amount,
                Type = "Withdraw"
            };
            _accContext.Transactions.Add(trans);
            await _accContext.SaveChangesAsync();

            ViewBag.Message = "Withdrawal Successful!";
            return View();
        }
        public async Task<IActionResult> ShowBalance()
        {
            int accNo = Convert.ToInt32(TempData["AccountNumber"]);
            TempData.Keep();

            var balance = await _accContext.Transactions
                .Where(t => t.AccountNumber == accNo)
                .SumAsync(t => t.Type == "Deposit" ? t.Amount : -t.Amount);

            ViewBag.Balance = balance;
            return View();
        }
        public async Task<IActionResult> MiniStatement()
        {
            int accNo = Convert.ToInt32(TempData["AccountNumber"]);
            TempData.Keep();

            var last10 = await _accContext.Transactions
                .Where(t => t.AccountNumber == accNo)
                .OrderByDescending(t => t.TransactionDate)
                .Take(10)
                .ToListAsync();

            return View(last10);
        }
    }
}
