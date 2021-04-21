drop procedure if exists sp_tbl_upload_request;
create procedure sp_tbl_upload_request()
begin
    declare currentSchema varchar(100);
    select DATABASE() into currentSchema;
    if not exists(
            select 1
            from information_schema.TABLES
            where TABLE_SCHEMA = currentSchema
              and TABLE_NAME = 'tbl_upload_request'
        ) then
        create table tbl_upload_request
        (
            id          int primary key auto_increment,
            total_parts int not null,
            file_hash   text,
            status      int not null,
            is_active   tinyint   default 1,
            created_by  int not null,
            created     timestamp default current_timestamp,
            modified_by int       default null,
            modified    timestamp default null
        );
    end if;
    if not exists(
            select 1
            from information_schema.COLUMNS
            where TABLE_SCHEMA = currentSchema
              and TABLE_NAME = 'tbl_upload_request'
              and COLUMN_NAME = 'file_owner'
        ) then
        alter table tbl_upload_request
            add column file_owner int not null after file_hash;
    end if;
    if not exists(
            select 1
            from information_schema.COLUMNS
            where TABLE_SCHEMA = currentSchema
              and table_name = 'tbl_upload_request'
              and COLUMN_NAME = 'file_name'
        ) then
        alter table tbl_upload_request add column file_name varchar(255) not null after file_owner;
    end if;
end;
call sp_tbl_upload_request();
drop procedure if exists sp_tbl_upload_request;