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
                        return Results.Ok(res);
                    case System.Net.HttpStatusCode.NotFound:
                        return Results.NotFound(res);
                    case System.Net.HttpStatusCode.Unauthorized:
                        return Results.Unauthorized();
                    case System.Net.HttpStatusCode.BadRequest:
                        return Results.BadRequest();
                    case System.Net.HttpStatusCode.Created:
                        return Results.Created("", res);
                    case System.Net.HttpStatusCode.InternalServerError:
                        return Results.Problem();
                    default:
                        return Results.Json(res);
                }
            }
        }
    }
}