drop procedure if exists sp_CreateUploadParts;
create procedure sp_CreateUploadParts(parRequestId int, parPartValue text, parPartNumber int, parUserId int)
begin
    set @isExists = 0;
    select id into @isExists from tbl_upload_request where id = parRequestId and is_active = 1;
    if @isExists > 0 then
        set @partCount = 0;
        select count(1)
        into @partCount
        from tbl_upload_request_parts
        where request_id = parRequestId
          and is_active = 1;
        if @partCount = 0 then
            update tbl_upload_request
            set status=2,
                modified_by=parUserId,
                modified=now()
            where id = parRequestId
              and is_active = 1;
        end if;
        insert into tbl_upload_request_parts(request_id, part_number, part_value, created_by)
            value (parRequestId, parPartNumber, parPartValue, parUserId);
        select last_insert_id() as id;
    else
        select -1 as id;
    end if;
end;