drop procedure if exists sp_Login;
create procedure sp_Login(parEmail varchar(255), parPassword varchar(255))
begin
    set @isExists = -1;
    select id
    into @isExists
    from tbl_credentials_master
    where email_address = parEmail
      and password_hash = parPassword
      and is_active = 1;
    if @isExists > 0 then
        select u.id,
               u.first_name,
               u.last_name,
               u.email_address,
               u.phone_number
        from tbl_user_master u
                 inner join tbl_credentials_master c
                            on c.email_address = u.email_address
                                and u.is_active = 1 and c.is_active = 1
        where u.email_address=parEmail and c.password_hash=parPassword;
    else
        select -1 as id;
    end if;
end;