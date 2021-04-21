drop procedure if exists sp_GetUploadRequestParts;
create procedure sp_GetUploadRequestParts(parRequestId int)
begin
    select r.id,
           r.file_owner,
           u.first_name,
           u.last_name,
           r.file_name,
           r.file_hash,
           p.part_number,
           p.part_value
    from tbl_upload_request r
             inner join tbl_upload_request_parts p
                        on p.request_id = r.id
             inner join tbl_user_master u
                        on r.file_owner = u.id
    where r.id = parRequestId
      and r.is_active = 1
    order by p.part_number;
end;