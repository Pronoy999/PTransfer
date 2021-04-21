drop procedure if exists sp_tbl_status_master;
create procedure sp_tbl_status_master()
begin
    declare currentSchema varchar(100);
    select DATABASE() into currentSchema;
    if not exists(
            select 1
            from information_schema.TABLES
            where TABLE_SCHEMA = currentSchema
              and TABLE_NAME = 'tbl_status_master'
        ) then
        create table tbl_status_master
        (
            id          int primary key auto_increment,
            status_name varchar(255) not null,
            is_active   tinyint   default 1,
            created_by  int          not null,
            created     timestamp default current_timestamp,
            modified_by int       default null,
            modified    timestamp default null
        );
    end if;
end;
call sp_tbl_status_master();
drop procedure if exists sp_tbl_status_master;