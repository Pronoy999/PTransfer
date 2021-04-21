drop procedure if exists sp_GetUploadRequestDetails;
create procedure sp_GetUploadRequestDetails(parRequestId int)
begin
    select r.id,
           r.total_parts,
           r.file_hash,
           r.file_owner,
           u.first_name,
           u.last_name,
           r.file_name,
           r.status,
           sm.status_name
    from tbl_upload_request r
             left join tbl_status_master sm
                       on sm.id = r.status
             left join tbl_user_master u
                       on u.id = r.file_owner
    where r.id = parRequestId
      and r.is_active = 1;
end;