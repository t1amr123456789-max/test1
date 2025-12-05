Trainer Dashboard API - Detailed Endpoints Tree

Legend: Roles in parentheses — Admin, Trainer, Client, Public

ADMIN AREA (18 endpoints total)

1) AdminController (12 endpoints)
 - GET /api/admin/programs/pending .................................. (Roles: Admin)
 - PUT /api/admin/programs/{programId}/approve........................ (Roles: Admin)
 - PUT /api/admin/programs/{programId}/reject ......................... (Roles: Admin)
 - DELETE /api/admin/programs/{programId}............................. (Roles: Admin)
 - GET /api/admin/exercise-library/pending........................... (Roles: Admin)
 - PUT /api/admin/exercise-library/{exerciseId}/approve .............. (Roles: Admin)
 - PUT /api/admin/exercise-library/{exerciseId}/reject ............... (Roles: Admin)
 - DELETE /api/admin/exercise-library/{exerciseId} .................... (Roles: Admin)
 - GET /api/admin/trainers/pending .................................. (Roles: Admin)
 - PUT /api/admin/trainers/{trainerId}/verify........................ (Roles: Admin)
 - PUT /api/admin/trainers/{trainerId}/reject ......................... (Roles: Admin)
 - DELETE /api/admin/trainers/{trainerId}............................ (Roles: Admin)

Controller total:12 endpoints

2) UsersController (4 endpoints)
 - GET /api/admin/users ............................................. (Roles: Admin)
 - GET /api/admin/users/{userId} ................................... (Roles: Admin)
 - PUT /api/admin/users/{userId}/role.............................. (Roles: Admin)
 - DELETE /api/admin/users/{userId} ................................ (Roles: Admin)

Controller total:4 endpoints

3) SubscriptionsController (2 endpoints)
 - GET /api/admin/subscriptions .................................... (Roles: Admin)
 - PUT /api/admin/subscriptions/{subscriptionId}/status ............. (Roles: Admin)

Controller total:2 endpoints

Admin area total endpoints:18


TRAINER AREA (38 endpoints total)

4) ExerciseLibraryController (6 endpoints)
 - GET /api/trainer/exerciselibrary............................... (Roles: Trainer)
 - GET /api/trainer/exerciselibrary/{id}.......................... (Roles: Trainer)
 - GET /api/trainer/exerciselibrary/search?name={name} ............. (Roles: Trainer)
 - POST /api/trainer/exerciselibrary............................... (Roles: Trainer)
 - PUT /api/trainer/exerciselibrary/{id}.......................... (Roles: Trainer)
 - DELETE /api/trainer/exerciselibrary/{id}........................ (Roles: Trainer)

Controller total:6 endpoints

5) ProgramsController (6 endpoints)
 - GET /api/trainer/programs ..................................... (Roles: Trainer)
 - GET /api/trainer/programs/search?term={term} ................... (Roles: Trainer)
 - GET /api/trainer/programs/{id} ................................ (Roles: Trainer)
 - POST /api/trainer/programs ..................................... (Roles: Trainer)
 - PUT /api/trainer/programs/{id} ................................ (Roles: Trainer)
 - DELETE /api/trainer/programs/{id}.............................. (Roles: Trainer)

Controller total:6 endpoints

6) WeeksController (5 endpoints)
 - GET /api/trainer/weeks/by-program/{programId} .................. (Roles: Trainer)
 - GET /api/trainer/weeks/{id} ................................... (Roles: Trainer)
 - POST /api/trainer/weeks ........................................ (Roles: Trainer)
 - PUT /api/trainer/weeks/{id} ................................... (Roles: Trainer)
 - DELETE /api/trainer/weeks/{id} ................................ (Roles: Trainer)

Controller total:5 endpoints

7) DaysController (5 endpoints)
 - GET /api/trainer/days/by-week/{weekId}........................ (Roles: Trainer)
 - GET /api/trainer/days/{id} .................................... (Roles: Trainer)
 - POST /api/trainer/days ......................................... (Roles: Trainer)
 - PUT /api/trainer/days/{id} .................................... (Roles: Trainer)
 - DELETE /api/trainer/days/{id} .................................. (Roles: Trainer)

Controller total:5 endpoints

8) DayExercisesController (5 endpoints)
 - GET /api/trainer/dayexercises/by-day/{dayId} ................... (Roles: Trainer)
 - GET /api/trainer/dayexercises/{id}............................. (Roles: Trainer)
 - POST /api/trainer/dayexercises .................................. (Roles: Trainer)
 - PUT /api/trainer/dayexercises/{id}............................. (Roles: Trainer)
 - DELETE /api/trainer/dayexercises/{id}.......................... (Roles: Trainer)

Controller total:5 endpoints

9) ProfileController (2 endpoints)
 - GET /api/trainer/profile/{trainerId}.......................... (Roles: Trainer, Client, Admin)
 - PUT /api/trainer/profile/{trainerId}.......................... (Roles: Trainer, Admin)

Controller total:2 endpoints

10) ClientsController (1 endpoint)
 - GET /api/trainer/{trainerId}/clients.......................... (Roles: Trainer, Admin)

Controller total:1 endpoint

11) ChatController (7 endpoints)
 - POST /api/trainer/chat/threads/start........................... (Roles: Trainer, Client)
 - GET /api/trainer/chat/threads/{trainerId} ...................... (Roles: Trainer)
 - POST /api/trainer/chat/messages/send........................... (Roles: Trainer, Client)
 - GET /api/trainer/chat/messages/thread/{threadId} .............. (Roles: Trainer, Client)
 - DELETE /api/trainer/chat/messages/{messageId}/delete-for-me .... (Roles: Trainer, Client)
 - DELETE /api/trainer/chat/messages/{messageId}/delete-for-all ... (Roles: Trainer, Client)
 - POST /api/trainer/chat/threads/{threadId}/seen .................. (Roles: Trainer, Client)

Controller total:7 endpoints

12) TrainerProfileController (legacy) (1 endpoint)
 - GET /api/trainer/trainerprofile/GetAllProfiles ................. (Roles: Trainer, Client, Admin)

Controller total:1 endpoint

Trainer area total endpoints:38


CLIENT / PUBLIC AREA (5 endpoints total)

13) HomeClientController (5 endpoints)
 - GET /api/client/homeclient/search?term={term} .................. (Roles: Client, Admin)
 - GET /api/client/homeclient/programs............................ (Roles: Client, Admin)
 - GET /api/client/homeclient/programs/{id} ....................... (Roles: Client, Admin)
 - GET /api/client/homeclient/trainers............................ (Roles: Client, Admin)
 - GET /api/client/homeclient/trainers/{id} ....................... (Roles: Client, Admin)

Controller total:5 endpoints

Client area total endpoints:5


AUTH / ACCOUNT (Public API) (2 endpoints total)

14) AccountController (2 endpoints)
 - POST /api/account/register .................................... (Roles: Public)
 - POST /api/account/login ....................................... (Roles: Public)

Controller total:2 endpoints


GRAND TOTAL
- Admin area:18 endpoints
- Trainer area:38 endpoints
- Client area:5 endpoints
- Auth/Public:2 endpoints

Total endpoints in system:63

Notes
- Role assignments are inferred from route areas and typical responsibilities; if you want strict RBAC mapping I can add [Authorize] attributes and policies accordingly.
- If you want an alternate export format (JSON / CSV / Postman collection), tell me which format and I will generate it.
