drop procedure if exists sp_UpdateUserDetails;
create procedure sp_UpdateUserDetails(parUserId int, parFirstName varchar(255), parLastName varchar(255),
                                      parEmail varchar(255), parPassword varchar(255))
begin
    if parUserId = 0 then
        select -1 as id;
    else
        set @setClaus = '';
        if length(parFirstName) > 0 then
            set @setClaus = concat('first_name = ''', parFirstName, ''',');
        end if;
        if length(parLastName) > 0 then
            set @setClaus = concat(@setClaus, 'last_name = ''', parLastName, ''',');
        end if;
        if length(parPassword) > 0 and length(parEmail) > 0 then
            update tbl_credentials_master
            set password_hash=parPassword,
                modified=now(),
                modified_by=parUserId
            where email_address = parEmail
              and is_active = 1;
        end if;
        if length(@setClaus) > 0 then
            select concat('update tbl_user_master set ', @setClaus, ' modified_by = ', parUserId, ', modified=now()')
            into @stmtSQL;
            #select @stmtSQL;
            prepare stmtExec from @stmtSQL;
            execute stmtExec;
            deallocate prepare stmtExec;
        end if;
        select 1 as id;
    end if;
end;