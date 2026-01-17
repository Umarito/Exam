var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAppointmentService, AppointmentService>();
builder.Services.AddScoped<IDoctorService, DoctorService>();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IQueueEventService, QueueEventService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IScheduleSlotService, ScheduleSlotService>();
builder.Services.AddScoped<ApplicationDBContext>();
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddLogging(config =>
{
    config.AddConsole();
    config.SetMinimumLevel(LogLevel.Information);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();

// create table patients(
// id serial primary key,
// fullname varchar not null,
// phone varchar unique not null,
// birth_date date null,
// is_active bool default(true),
// created_at timestamp default(now())
// );
// create table doctors(
// id serial primary key,
// fullname varchar not null,
// specialty varchar not null,
// is_active bool default(true),
// hired_at date not null
// );
// create table rooms(
// id serial primary key,
// name varchar not null,
// is_active bool default(true)
// );
// create table schedule_slots(
// id serial primary key,
// doctor_id int references doctors(id),
// room_id int references rooms(id),
// start_time timestamp not null,
// end_time timestamp not null,
// is_active bool default(true)
// );
// create type app_status as enum('booked','checked_in','in_progress','done','cancelled');
// create table appointments(
// id serial primary key,
// patient_id int references patients(id),
// slot_id int references schedule_slots(id),
// status app_status,
// created_at timestamp default(now()),
// updated_at timestamp default(now())
// );
// create type eventt as enum('booked','checked_in','started','finished','cancelled');
// create table queue_events(
// id serial primary key,
// appointment_id int references appointments(id),
// event_type eventt,
// created_at timestamp default(now())
// );