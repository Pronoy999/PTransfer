drop procedure if exists sp_tbl_credentials_master;
create procedure sp_tbl_credentials_master()
begin
    declare currentSchema varchar(100);
    select DATABASE() into currentSchema;
    if not exists(
            select 1
            from information_schema.TABLES
            where TABLE_NAME = 'tbl_credentials_master'
              and TABLE_SCHEMA = currentSchema
        ) then
        begin
            create table tbl_credentials_master
            (
                id            int primary key auto_increment,
                email_address varchar(255) not null,
                password_hash text         not null,
                is_active     tinyint   default 1,
                created_by    int          not null,
                created       timestamp default current_timestamp,
                modified_by   int       default null,
                modified      timestamp default null
            );
        end;
    end if;
end;
call sp_tbl_credentials_master();
drop procedure if exists sp_tbl_credentials_master;