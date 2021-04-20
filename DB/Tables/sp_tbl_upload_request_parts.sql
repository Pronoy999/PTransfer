drop procedure if exists sp_tbl_upload_request_parts;
create procedure sp_tbl_upload_request_parts()
begin
    declare currentSchema varchar(100);
    select DATABASE() into currentSchema;
    if not exists(
            select 1
            from information_schema.TABLES
            where TABLE_SCHEMA = currentSchema
              and TABLE_NAME = 'tbl_upload_request_parts'
        ) then
        create table tbl_upload_request_parts
        (
            id          int primary key auto_increment,
            request_id  int  not null,
            part_value  text not null,
            is_active   tinyint   default 1,
            created_by  int  not null,
            created     timestamp default current_timestamp,
            modified_by int       default null,
            modified    timestamp default null
        );
    end if;
end;
call sp_tbl_upload_request_parts();
drop procedure if exists sp_tbl_upload_request_parts;