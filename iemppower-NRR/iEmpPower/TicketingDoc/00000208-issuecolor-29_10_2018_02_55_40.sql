declare @beginddate smalldatetime='2018-10-08 12:13:00',@enddate smalldatetime, @currentddate smalldatetime=CAST(getdate()+20 as smalldatetime), @totaldatediffernce decimal,@tilldatediffernce decimal, @percentage float=0.0
set @enddate=@beginddate+8
set @totaldatediffernce=Cast((DATEDIFF(ms, @beginddate, @enddate)) as decimal(38,2))
set @tilldatediffernce=Cast((DATEDIFF(ms, @beginddate, @currentddate)) as decimal(38,2))
set @percentage = (@tilldatediffernce/@totaldatediffernce)*100
print @percentage
Begin
	if(@percentage<=40)
	begin
		print 'Green'
	end
	else if(@percentage>40 and @percentage<=80)
	begin
		print 'Amber'
	end
	else if(@percentage>80 )
	begin
		print 'red'
	end
	else
	begin
		print 'Completed'
	end

End

