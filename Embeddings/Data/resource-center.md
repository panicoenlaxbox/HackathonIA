---
title: "Resource center"
date: 2024-04-11
geekdocCollapseSection: true
---

El grupo de recursos del resource center lleva:

- MySql
- App service plan
- Varios app services
- Container registry
- Key vault
- Storage account

## ¿Qué se guarda?

Copia de la base de datos y de los app services

¿Del resto?

## ¿Cómo se guardan los backups?

- Base de datos:

Nos apoyamos en lo ofrecido por el propio servicio *Azure Database for MySQL flexible server*
Se hace un backup completo diario, manteniendo una retención de 1 semana.


- App service

Se realizan cada hora con una retención de 1 mes, usando la configuración automática.

