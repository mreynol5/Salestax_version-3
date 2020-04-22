using System . Collections . Generic;
using System . Diagnostics;
using Microsoft . AspNetCore . Mvc;
using Newtonsoft . Json . Linq;
using SalesTax . DataAccess . Models . TaxJar;
using SalesTax . Models;

namespace SalesTax . Controllers
{
    [ApiController]
    [Route("Home")]
    public class HomeController : Controller
        {
        private JObject jo = new JObject ( );
        private IProductsReqRepo _itemsRepository;
        private RequestBuilder rb = new RequestBuilder ( );
        private TJRequest tjreq = new TJRequest ( );

        public HomeController ( IProductsReqRepo productsReqRepo)
        {
            _itemsRepository = productsReqRepo;
        }

        [HttpGet]
        public IActionResult Index ( )
        {
            var cart = _itemsRepository . DisplayAllItems ( );

            return View ( cart );
        }

        [HttpGet]
        [Route ( "gettax" )]
        public ViewResult GetTax ()
            {
            IEnumerable<IProductsReqRepo> invoiceItems = _itemsRepository . GetAllItems ( );
            return View ( invoiceItems );
            }


        public IActionResult Privacy ( )
            {
            return View ( );
            }

        public IEnumerable<IProductsReqRepo> GetItem (int Id )
            {
            Id = 1038;
            IEnumerable<IProductsReqRepo> item = _itemsRepository.GetItem ( Id );
            return item;
            }

        [HttpGet]
        [Route("listItems")]
        public ViewResult items ( )
            {
            IEnumerable<IProductsReqRepo> cart = _itemsRepository . GetAllItems ( );
            ViewData [ "Cart" ] = cart;
            ViewData [ "PageTitle" ] = "Here are the contents of your Shopping Cart.";
            return View ( cart );
            }

        [ResponseCache ( Duration = 0 , Location = ResponseCacheLocation . None , NoStore = true )]
        public IActionResult Error ( )
            {
            return View ( new ErrorViewModel { RequestId = Activity . Current?.Id ?? HttpContext . TraceIdentifier } );
            }
        }
    }
