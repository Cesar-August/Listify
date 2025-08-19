using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TodoListApp.Models; // ajuste conforme seu namespace

public class CalendarController : Controller
{
    public ActionResult Semana()
    {
        var eventos = new List<EventModel>
        {
            new EventModel { Date = new DateTime(2025, 5, 15, 15, 15, 0), Title = "1515", Description = "Evento de teste" },
            new EventModel { Date = new DateTime(2025, 5, 14), Title = "55555", Description = "Outro evento" }
        };

        return View(eventos);
    }
}
