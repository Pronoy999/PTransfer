drop procedure if exists sp_CreateOrUpdateUploadRequest;
create procedure sp_CreateOrUpdateUploadRequest(parRequestId int, parParts int, parFileHash text,
                                                parFileOwner int, parFileName varchar(255), parRequestStatus int)
begin
    if (parRequestId > 0) then
        update tbl_upload_request
        set status=parRequestStatus,
            total_parts=(select count(1)
                         from tbl_upload_request_parts
                         where request_id = parRequestId
                           and tbl_upload_request_parts.is_active = 1),
            modified=now(),
            modified_by=parFileOwner
        where id = parRequestId;
        select parRequestId as request_id;
    else
        #Creating the request with initiated status.
        insert into tbl_upload_request(total_parts, file_hash, file_owner, file_name, status, created_by)
            value (parParts, parFileHash, parFileOwner, parFileName, 1, parFileOwner);
        select last_insert_id() as request_id;
    end if;
end;