﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using HotChocolateSample.Core;
using HotChocolateSample.GraphQL;
using Newtonsoft.Json.Linq;

namespace HotChocolateSample.Controllers
{
    public class CompanyController : Controller
    {
        // GET: Company
        public async Task<ActionResult> Index()
        {
            var query = @"
                query { 
                    companies {
                        nodes {
                            id
                            name
                            revenue
                        }
                    }
                }
            ";
            HttpClient client = new HttpClient{BaseAddress = new Uri(BackEndConstants.GraphQLUrl)};
            var response = await client.GetStringAsync($"?query={query}");
            var json = JObject.Parse(response);
            var companiesJson = json["data"]["companies"]["nodes"];
            List<Company> companies = new List<Company>();
            foreach (var obj in companiesJson)
            {
                companies.Add(new Company()
                {
                    Id = int.Parse(obj["id"].ToString()),
                    Name = obj["name"].ToString(),
                    Revenue = decimal.Parse(obj["revenue"].ToString())
                });
            }
            return View(companies);
        }

        // GET: Company/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Company/Create
        public ActionResult Create()
        {
            return View();
        }


        // TODO Implement Create
        // POST: Company/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(IFormCollection collection)
        {
            string mutation = $@"
                query:
                mutation {{
                    createCompany(inputCompany: {{
                        name: ""{collection["name"]}"",
                        revenue: {collection["revenue"]}
                    }})
                    {{
                        id
                        name
                        revenue
                    }}
                }}
            ";

            HttpClient client = new HttpClient();
            
            var result = await client.SendAsync(new HttpRequestMessage()
            {
                Content = new StringContent(mutation),Method = HttpMethod.Post,RequestUri = new Uri(BackEndConstants.GraphQLUrl)
            });

            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Company/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Company/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Company/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Company/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}