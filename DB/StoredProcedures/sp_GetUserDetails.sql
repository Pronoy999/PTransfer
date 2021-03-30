drop procedure if exists sp_GetUserDetails;
create procedure sp_GetUserDetails(parUserId int, parEmailId varchar(255))
begin
    set @whereClaus = '';
    if parUserId > 0 then
        set @whereClaus = concat(' id = ', parUserId);
    end if;
    if length(parEmailId) > 0 then
        set @whereClaus = concat(' email_address = ''', parEmailId, '''');
    end if;
    select concat('select id,
           first_name,
           last_name,
           email_address,
           phone_number,
           is_active
    from tbl_user_master where', @whereClaus, ' and is_active = 1')
    into @stmtSQL;
    #select @stmtSQL;
    prepare stmtExec from @stmtSQL;
    execute stmtExec;
    deallocate prepare stmtExec;
end;