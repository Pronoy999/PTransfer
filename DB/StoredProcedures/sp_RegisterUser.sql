drop procedure if exists sp_RegisterUser;
create procedure sp_RegisterUser(parFirstName varchar(255), parLastName varchar(255), parEmail varchar(255),
                                 parPhone varchar(15), parPassword text)
begin
    set @isExists = 0;
    set @userId = 0;
    select id into @isExists from tbl_user_master where email_address = parEmail and is_active = 1;
    if @isExists = 0 then
        insert into tbl_user_master(first_name, last_name, email_address, phone_number, created_by)
            value (parFirstName, parLastName, parEmail, parPhone, 1);
        select last_insert_id() into @userId;
        insert into tbl_credentials_master(email_address, password_hash, created_by)
            value (parEmail, parPassword, @userId);
        select @userId as id;
    else
        select -1 as id;
    end if;
end;
