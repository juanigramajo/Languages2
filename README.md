<!-- # tl2-tp10-2023-juanigramajo-->

## ESPAÑOL:

# Proyecto final - Taller de Lenguajes II

---

- Gramajo Juan Ignacio
- Licenciatura en Informática

---

(Este proyecto fué realizado en 2024).

_Proyecto final de Taller de Lenguajes II, este proyecto es sobre un tablero Kanban._


## Informe:

### 1. Pestañas
- Sin estar loggeado, únicamente puede acceder a crear un usuario o a iniciar sesión, todas las demás redireccionan a loggearse.
- Si no tiene usuario, al crearse uno únicamente se puede asignar el rol de un operador.

### 2. Permisos de sesión
- Un admin:
    - Puede crear usuarios con cualquier rol.
    - Puede editar y eliminar usuarios (no puede eliminar su propio usuario ni bajar su propio rango de rol).
    - Puede crear, editar y eliminar tableros.
    - Puede crear, editar y eliminar tareas.
- Un operador:
    - No puede editar ni eliminar usuarios.
    - No puede editar ni eliminar tareas que no le fueron asignados.
    - No puede editar ni eliminar tableros que no le fueron asignados.
    - Puede crear un tablero y tareas.

### 3. Visualizaciones de tableros
Los tableros estan organizados en 3 secciones
- Mis tableros: Donde puede ver los tableros creados por el usuario.
- Mis tareas en otros tableros: Tableros que no fueron creados por el usuario pero donde tiene tareas asignadas.
- Explorar más tableros: Tableros que no fueron creados por el usuario y no tiene tareas en él.

### 4. Visualizaciones de tareas
Las tareas estan organizadas en 2 secciones por tableros
- Mis tareas en el tablero: Las cuales puede editar o eliminar.
- Listado de otras tareas en el tablero: Las cuales solo puede ver.

---

## ENGLISH:

# Final project - Language Workshop II

---

- Gramajo Juan Ignacio
- Computer science student

---

(This project was made in 2024).

_Final project of Language Workshop II, this project is on a Kanban board._


## Report:

### 1. Eyelashes
- Without being registered, you can only access to create a user or log in, all others are redirected to log in.
- If you do not have a user, when you create one you can only assign the role of an operator.

### 2. Session permissions
- An administrator:
 - You can create users with any role.
 - You can edit and delete users (you cannot delete your own user or lower your own role rank).
 - You can create, edit and delete boards.
 - You can create, edit and delete tasks.
- An operator:
 - You cannot edit or delete users.
 - You cannot edit or delete tasks that were not assigned.
 - You cannot edit or delete boards that were not assigned to you.
 - You can create a board and tasks.

### 3. Dashboard visualizations
The boards are organized into 3 sections.
- My boards: Where you can see the boards created by the user.
- My tasks on other boards: Boards that were not created by the user but where they have assigned tasks.
- Explore more boards: Boards that were not created by the user and do not have tasks on it.

### 4. Task visualizations
The tasks are organized into 2 sections by boards.
- My tasks on the board: Which you can edit or delete.
- List of other tasks on the board: Which you can only see.




<!-- ## Corregir:
- En vez de escribir las funciones isLogger e isAdmin en cada controller usar un helper, en una clase estática.
- Que pide required en formularios cuando no esta solicitado en el viewmodel
- Hacer funcion Login en Usuario Repository (??)
- Poder tener tareas asignadas a mas usuarios (?)
- Que no se puedan crear tableros y tareas ya existentes.
- Cuando elimino un usuario, el tablero y tareas deben tener idUsuario en -9999


---

## Se puede agregar:
Ofrecer colores y poder agregar colores en tareas

---

## Consultar:
- Problema del selected en los formularios de editar tarea y usuario (estado y rol) (basically cuando toco editar, no se carga el dato, se pone el marcado en el selected)
    - (solucion momentanea, quitar el selected del editar usuario)
    - (segunda solucion momentanea, poner el selected en el primer valor)
- Cuando intente usar el index de tarea para mostrarlas por tablero no me dejaba, por eso tuve que crear otro index de tareas en una view en tableros.
 -->