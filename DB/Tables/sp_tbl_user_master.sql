drop procedure if exists sp_tbl_user_master;
create procedure sp_tbl_user_master()
begin
    declare currentSchema varchar(100);
    select DATABASE() into currentSchema;
    if not exists(
            select 1
            from information_schema.TABLES
            where TABLE_SCHEMA = currentSchema
              and TABLE_NAME = 'tbl_user_master'
        ) then
        begin
            create table tbl_user_master
            (
                id          int auto_increment primary key,
                first_name  varchar(255)          not null,
                last_name   varchar(255)          not null,
                email_address       varchar(255)          not null,
                phone_number       varchar(15) default null,
                is_active   tinyint     default 1 not null,
                created_by  int                   not null,
                created     timestamp   default current_timestamp,
                modified_by int         default null,
                modified    timestamp   default null
            );
        end;
    end if;
end;
call sp_tbl_user_master();
drop procedure if exists sp_tbl_user_master;