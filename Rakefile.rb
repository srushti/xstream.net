require 'rubygems'
require 'rake'
require 'CSProjFile.rb'

task :default => [:compile, :test]

task :compile do
  csproj_file = CSProjFile.new('XStream/XStream.csproj')
  fail_on_error (csproj_file.create_csc, "compile")
end

task :test do
end

def fail_on_error(command, name)
  task_successful = system command
  raise ("Command " + name + " failed") if !task_successful
end
