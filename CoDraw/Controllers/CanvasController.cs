using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;

namespace CoDraw.Controllers {
    [ApiController]
    public class CanvasController : Controller {

        private static byte[] canvas;
        private static string header;
        [HttpGet("getCanvas")]
        public ActionResult<string> GetCanvas() {
            if (canvas != null && header != null) return new ObjectResult(header + "," + Convert.ToBase64String(canvas));
            return new EmptyResult();
        }
        [HttpPost("postCanvas")]
        public ActionResult PostCanvas([FromBody] string base64canvas) {
            canvas = Convert.FromBase64String(base64canvas.Split(',')[1]);
            header = base64canvas.Split(',')[0];
            return new OkResult();
        }
    } 
}
namespace CoDraw { 
    public class UpdateHub : Hub {
        public async Task Notify() {
            await this.Clients.All.SendAsync("Notify");
        }
    }
}

