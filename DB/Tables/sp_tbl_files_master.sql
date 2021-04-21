drop procedure if exists sp_tbl_files_master;
create procedure sp_tbl_files_master()
begin
    declare currentSchema varchar(100);
    select DATABASE() into currentSchema;
    if not exists(
            select 1
            from information_schema.TABLES
            where TABLE_SCHEMA = currentSchema
              and TABLE_NAME = 'tbl_files_master'
        ) then
        create table tbl_files_master
        (
            id          int primary key auto_increment,
            file_name   varchar(255),
            file_link   text,
            request_id  int not null,
            file_owner  int,
            is_active   tinyint   default 1,
            created_by  int not null,
            created     timestamp default current_timestamp,
            modified_by int       default null,
            modified    timestamp default null
        );
    end if;
end;
call sp_tbl_files_master();
drop procedure if exists sp_tbl_files_master;