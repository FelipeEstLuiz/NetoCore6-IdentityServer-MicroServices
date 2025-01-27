﻿using MicroServices.Web.Models;
using MicroServices.Web.Services.IServices;
using MicroServices.Web.Utils;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MicroServices.Web.Services;

public class CouponService : ICouponService
{
    private readonly HttpClient _client;
    public const string BasePath = "api/v1/coupon";

    public CouponService(HttpClient client)
    {
        _client = client ?? throw new ArgumentNullException(nameof(client));
    }

    public async Task<CouponViewModel> GetCouponAsync(string code, string token)
    {
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await _client.GetAsync($"{BasePath}/{code}");
        if (response.StatusCode != HttpStatusCode.OK) return new CouponViewModel();
        return await response.ReadContentAs<CouponViewModel>();
    }
}
