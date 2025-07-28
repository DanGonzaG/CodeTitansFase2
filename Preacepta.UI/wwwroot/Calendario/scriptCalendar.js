let eventsArr = [];

document.addEventListener("DOMContentLoaded", async () => {
    const calendar = document.querySelector(".calendar");
    const date = document.querySelector(".date");
    const daysContainer = document.querySelector(".days");
    const prev = document.querySelector(".prev");
    const next = document.querySelector(".next");
    const todayBtn = document.querySelector(".today-btn");
    const gotoBtn = document.querySelector(".goto-btn");
    const dateInput = document.querySelector(".date-input");
    const eventDay = document.querySelector(".event-day");
    const eventDate = document.querySelector(".event-date");
    const eventsContainer = document.querySelector(".events");
    const addEventWrapper = document.querySelector(".add-event-wrapper");
    const addEventCloseBtn = document.querySelector(".close");

    let today = new Date();
    let activeDay;
    let month = today.getMonth();
    let year = today.getFullYear();

    const monthsIngles = [
        "January", "February", "March", "April", "May", "June",
        "July", "August", "September", "October", "November", "December"
    ];

    const months = [
        "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
        "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"
    ];

    // ========= FUNCIONES PRINCIPALES =========
    async function getEventsFromServer() {
        try {
            const response = await fetch("/Citas/ObtenerCitas");
            const citas = await response.json();
            eventsArr = [];
            citas.forEach(cita => {
                const [year, month, day] = cita.fecha.split("-").map(Number);
                console.log("DEBUG cita:", cita.fecha, { year, month, day });
                let dayObj = eventsArr.find(e => e.day === day && e.month === month && e.year === year);
                if (!dayObj) {
                    dayObj = { day, month, year, events: [] };
                    eventsArr.push(dayObj);
                }
                dayObj.events.push({
                    id: cita.idCita,
                    title: cita.nombreTipoCita || "Sin título",
                    time: cita.hora
                });
            });
        } catch (error) {
            console.error("Error cargando citas:", error);
        }
    }

    function initCalendar() {
        const firstDay = new Date(year, month, 1);
        const lastDay = new Date(year, month + 1, 0);
        const prevLastDay = new Date(year, month, 0);
        const prevDays = prevLastDay.getDate();
        const lastDate = lastDay.getDate();
        const day = firstDay.getDay();
        const nextDays = 7 - lastDay.getDay() - 1;

        date.innerHTML = `${months[month]} ${year}`;
        let days = "";

        for (let x = day; x > 0; x--) {
            days += `<div class="day prev-date">${prevDays - x + 1}</div>`;
        }

        for (let i = 1; i <= lastDate; i++) {
            let dayObj = eventsArr.find(e => e.day === i && e.month === month + 1 && e.year === year);
            if (i === today.getDate() && year === today.getFullYear() && month === today.getMonth()) {
                activeDay = i;
                getActiveDay(i);
                updateEvents(i);
                if (dayObj && dayObj.events.length > 0) {
                    days += `<div class="day today active event" data-id="${dayObj.events[0].id}">${i}</div>`;
                } else {
                    days += `<div class="day today active">${i}</div>`;
                }
            } else {
                if (dayObj && dayObj.events.length > 0) {
                    days += `<div class="day event" data-id="${dayObj.events[0].id}">${i}</div>`;
                } else {
                    days += `<div class="day">${i}</div>`;
                }
            }
        }

        for (let j = 1; j <= nextDays; j++) {
            days += `<div class="day next-date">${j}</div>`;
        }

        daysContainer.innerHTML = days;
        addDayListeners();
    }

    function addDayListeners() {
        const days = document.querySelectorAll(".day");
        days.forEach(day => {
            day.addEventListener("click", async e => {
                const clickedDay = Number(e.target.textContent);
                if (e.target.classList.contains("prev-date")) {
                    month--;
                    if (month < 0) { month = 11; year--; }
                } else if (e.target.classList.contains("next-date")) {
                    month++;
                    if (month > 11) { month = 0; year++; }
                } else {
                    activeDay = clickedDay;
                }
                initCalendar();
                try {
                    await updateEvents(clickedDay);
                } catch (error) {
                    console.error("Error al actualizar eventos:", error);
                }
            });
        });
    }

    function getActiveDay(date) {
        const dayObj = new Date(year, month, date);
        const dayName = dayObj.toLocaleDateString("es-ES", { weekday: "long" });
        eventDay.textContent = dayName.charAt(0).toUpperCase() + dayName.slice(1);
        eventDate.textContent = `${date} ${months[month]} ${year}`;
    }

    async function updateEvents(date) {
        let fecha = `${year}-${String(month + 1).padStart(2, "0")}-${String(date).padStart(2, "0")}`;
        try {
            const response = await fetch(`/Citas/ObtenerCitasPorFecha?fecha=${fecha}`);
            const citas = await response.json();
            let eventsHTML = "";
            if (citas.length > 0) {
                citas.forEach(cita => {
                    eventsHTML += `<div class="event" data-id="${cita.idCita}">
                        <div class="title"><i class="fas fa-circle"></i><h3>${cita.nombreTipoCita || "Sin título"}</h3></div>
                        <div class="event-time"><span>${cita.hora}</span></div>
                    </div>`;
                });
            } else {
                eventsHTML = `<div class="no-event"><h3>Sin Citas</h3></div>`;
            }
            eventsContainer.innerHTML = eventsHTML;
            agregarClickEventos();
        } catch (error) {
            console.error("Error al cargar citas del día:", error);
            eventsContainer.innerHTML = `<div class="no-event"><h3>Error al cargar citas</h3></div>`;
        }
    }

    prev.addEventListener("click", () => { month--; if (month < 0) { month = 11; year--; } initCalendar(); });
    next.addEventListener("click", () => { month++; if (month > 11) { month = 0; year++; } initCalendar(); });
    todayBtn.addEventListener("click", () => { today = new Date(); month = today.getMonth(); year = today.getFullYear(); initCalendar(); });
    gotoBtn.addEventListener("click", () => {
        const [m, y] = dateInput.value.split("/").map(Number);
        if (m > 0 && m < 13 && y.toString().length === 4) {
            month = m - 1; year = y; initCalendar();
        } else alert("Fecha inválida");
    });

    async function agregarClickEventos() {
        const renderedEvents = document.querySelectorAll(".event");
        renderedEvents.forEach(ev => {
            ev.addEventListener("click", (e) => {
                e.stopPropagation();
                const citaId = ev.getAttribute("data-id");
                if (citaId) {
                    fetch(`/Citas/Details/${citaId}`)
                        .then(res => res.text())
                        .then(html => {
                            document.getElementById("detalleCitaBody").innerHTML = html;
                            document.getElementById("modalDetalleCita").style.display = "block";
                        })
                        .catch(err => {
                            console.error("Error al cargar detalles de cita", err);
                            alert("No se pudo cargar la información de la cita.");
                        });
                } else {
                    alert("ID de cita inválido");
                }
            });
        });
    }

    // Iniciar la aplicación
    await getEventsFromServer();
    initCalendar();
});
document.addEventListener("DOMContentLoaded", () => {
    const addEventBtn = document.querySelector(".add-event");
    const addEventWrapper = document.querySelector(".add-event-wrapper");
    const addEventCloseBtn = document.querySelector(".close");

    if (addEventBtn && addEventWrapper && addEventCloseBtn) {

        // Toggle para mostrar/ocultar el formulario de añadir evento
        addEventBtn.addEventListener("click", () => {
            addEventWrapper.classList.toggle("active");
        });

        // Cerrar formulario
        addEventCloseBtn.addEventListener("click", () => {
            addEventWrapper.classList.remove("active");
        });

        // Cerrar formulario si haces click fuera del formulario y el botón
        document.addEventListener("click", (e) => {
            if (e.target !== addEventBtn && !addEventWrapper.contains(e.target)) {
                addEventWrapper.classList.remove("active");
            }
        });

    } else {
        console.warn("No se encontraron los elementos necesarios para agregar eventos");
    }
})
function mostrarDetalles() {
    var osccred = document.createElement("div");
    osccred.innerHTML =
        "A Project By <a href='https://www.youtube.com/channel/UCiUtBDVaSmMGKxg1HYeK-BQ' target=_blank>Open Source Coding</a>";
    osccred.style.position = "absolute";
    osccred.style.bottom = "0";
    osccred.style.right = "0";
    osccred.style.fontSize = "10px";
    osccred.style.color = "#ccc";
    osccred.style.fontFamily = "sans-serif";
    osccred.style.padding = "5px";
    osccred.style.background = "#fff";
    osccred.style.borderTopLeftRadius = "5px";
    osccred.style.borderBottomRightRadius = "5px";
    osccred.style.boxShadow = "0 0 5px #ccc";
    document.body.appendChild(osccred);

    mostrarDetalles();
}
//allow only time in eventtime from and to
if (typeof addEventFrom !== 'undefined' && addEventFrom) {
    addEventFrom.addEventListener("input", (e) => {
        addEventFrom.value = addEventFrom.value.replace(/[^0-9:]/g, "");
        if (addEventFrom.value.length === 2) {
            addEventFrom.value += ":";
        }
        if (addEventFrom.value.length > 5) {
            addEventFrom.value = addEventFrom.value.slice(0, 5);
        }
    });
}
const formCrearCita = document.getElementById("formCrearCita");

if (formCrearCita) {
    formCrearCita.addEventListener("submit", (e) => {
        e.preventDefault(); // evita el envío hasta validar

        const eventTimeFrom = document.querySelector("#Hora")?.value || "";

        if (eventTimeFrom === "") {
            alert("Por favor, completa todos los campos");
            return;
        }

        const timeFromArr = eventTimeFrom.split(":");
        if (
            timeFromArr.length !== 2 ||
            +timeFromArr[0] > 23 ||
            +timeFromArr[1] > 59
        ) {
            alert("Formato de hora no válido");
            return;
        }

        const timeFrom = convertTime(eventTimeFrom);

        let eventExist = false;
        eventsArr.forEach((event) => {
            if (
                event.day === activeDay &&
                event.month === month + 1 &&
                event.year === year
            ) {
                event.events.forEach((e) => {
                    if (e.title === eventTitle) {
                        eventExist = true;
                    }
                });
            }
        });

        if (eventExist) {
            alert("Este evento ya está registrado");
            return;
        }

        const newEvent = {
            id: cita?.IdCita ?? null,
            title: eventTipoCita,
            time: timeFrom,
        };

        let eventAdded = false;
        eventsArr.forEach((item) => {
            if (
                item.day === activeDay &&
                item.month === month + 1 &&
                item.year === year
            ) {
                item.events.push(newEvent);
                eventAdded = true;
            }
        });

        if (!eventAdded) {
            eventsArr.push({
                day: activeDay,
                month: month + 1,
                year: year,
                events: [newEvent],
            });
        }

        updateEvents(activeDay);
        const activeDayEl = document.querySelector(".day.active");
        if (activeDayEl && !activeDayEl.classList.contains("event")) {
            activeDayEl.classList.add("event");
        }

        console.log(eventsArr);


        const inputHora = document.querySelector("#Hora");
        if (inputHora) inputHora.value = "";


        formCrearCita.submit();
    });
}

//function to save events in local storage
function saveEvents() {
    localStorage.setItem("events", JSON.stringify(eventsArr));
}

//function to get events from local storage
function getEvents() {
    //check if events are already saved in local storage then return event else nothing
    if (localStorage.getItem("events") === null) {
        return;
    }
    eventsArr.push(...JSON.parse(localStorage.getItem("events")));
}

function convertTime(time) {
    //convert time to 24 hour format
    let timeArr = time.split(":");
    let timeHour = timeArr[0];
    let timeMin = timeArr[1];
    let timeFormat = timeHour >= 12 ? "PM" : "AM";
    timeHour = timeHour % 12 || 12;
    time = timeHour + ":" + timeMin + " " + timeFormat;
    return time;
}

$(document).ready(function () {
    $('#ModalCitasFuturas').on('shown.bs.modal', function () {
        console.log($('#tablaCitasFuturas').length);
        $('#tablaCitasFuturas').DataTable({
            language: {
                paginate: {
                    previous: "Anterior",
                    next: "Siguiente"
                },
                emptyTable: "No hay datos disponibles en la tabla",
                info: "Mostrando _START_ a _END_ de _TOTAL_ entradas",
                lengthMenu: "Mostrar _MENU_ entradas",
                search: "Buscar:",
                zeroRecords: "No se encontraron resultados"
            }

        });
    });
});
