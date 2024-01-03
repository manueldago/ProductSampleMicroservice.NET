namespace sharedKernel
{
    public static class HttpCodeHelper
    {
        public static IResult Response(ResponseAPI? res)
        {
            if (res == null)
            {
                return Results.Problem();
            }
            else
            {
                switch (res.ResponseStatusCode)
                {
                    case System.Net.HttpStatusCode.OK:
                        Results.Ok(res);
                        break
                           ;
                    case System.Net.HttpStatusCode.NotFound:
                        Results.NotFound(res);
                        break;

                    case System.Net.HttpStatusCode.Unauthorized:
                        Results.Unauthorized();
                        break;

                    case System.Net.HttpStatusCode.BadRequest:
                        Results.BadRequest();
                        break;
                    case System.Net.HttpStatusCode.Created:
                        Results.Created("", res);
                        break;
                    case System.Net.HttpStatusCode.InternalServerError:
                        Results.Problem();
                        break;
                };
                return Results.Json(res);
            }
        }
    }
}